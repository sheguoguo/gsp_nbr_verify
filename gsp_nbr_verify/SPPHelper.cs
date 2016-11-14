using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;

namespace gsp_nbr_verify
{
    class SPPHelper
    {
        
        
        
        public static string owner_name_to_nbr(string owner_name)
        {
            string owner_nbr = "";
            switch (owner_name)
            {
                case "BT":
                    owner_nbr="320000000000124343";
                    break;
                case "SW":
                    owner_nbr = "c80aa76795794a84a57a1a7498bf94a7";
                    break;
                case "WS":
                    owner_nbr = "178ef21c96034398b06a90fefc68ef42";
                    break;
                default:
                    owner_nbr = "not_exist";
                    break;
            }
            return owner_nbr;
        }

        public static DataTable SPP_list_owner(string usb_key)
        {
            string corp_id = "<?xml version=\"1.0\"?><QueryOwnerCorpListRequest xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><entSeqNo>320000000000141855</entSeqNo></QueryOwnerCorpListRequest>";
            StringBuilder xml_key = new StringBuilder();
            xml_key.Append("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><RequestContext><Group name=\"SystemInfo\"><Key name=\"SessionId\">");
            //xml_key.Append(richTextBox1.Text.Substring(3,32));
            xml_key.Append(usb_key);
            xml_key.Append("</Key></Group><Group name=\"DataPresentation\"><Key name=\"SignatureAlgorithm\" /><Key name=\"EncryptAlgorithm\" /><Key name=\"CompressAlgorithm\" /></Group></RequestContext>");
            //base64 encode
            byte[] key_bytes = Encoding.Default.GetBytes(xml_key.ToString());
            byte[] corp_bytes = Encoding.Default.GetBytes(corp_id);
            byte[] result = new byte[1000];
            //string key_str_base64 = Convert.ToBase64String(key_bytes);
            //post to webservice
            com.drugadmin.sp.SuperPass ws = new gsp_nbr_verify.com.drugadmin.sp.SuperPass();
            ws.service("piats.superpass.tpl.QueryOwnerCorpListService", key_bytes, corp_bytes, out result);
            //DataSet ds=GetDataSetByXml(Encoding.GetEncoding("UTF-8").GetString(result));
            DataSet ds = XmlDatasetConvert.ConvertXMLToDataSet(Encoding.GetEncoding("UTF-8").GetString(result));
            return ds.Tables[0];
        }

        public static DataTable SPP_list_order(string usb_key,string asn_nbr,string owner_nbr)
        {
            StringBuilder order_id = new StringBuilder();
            order_id.Append("<?xml version=\"1.0\"?><QueryChkInOutListsRequest xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><chkInOutRequest><dataType>A</dataType><entStoreInoutId>");
            order_id.Append(asn_nbr);
            order_id.Append("</entStoreInoutId><entSeqNo>");
            order_id.Append(owner_nbr);
            order_id.Append("</entSeqNo><corpSeqNo>320000000000141855</corpSeqNo><corpName>国药集团医药物流有限公司</corpName></chkInOutRequest><PageResquest><curPage>1</curPage><pageSize>10</pageSize></PageResquest></QueryChkInOutListsRequest>");
            StringBuilder xml_key = new StringBuilder();
            xml_key.Append("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><RequestContext><Group name=\"SystemInfo\"><Key name=\"SessionId\">");
            //xml_key.Append(richTextBox1.Text.Substring(3,32));
            xml_key.Append(usb_key);
            
            xml_key.Append("</Key></Group><Group name=\"DataPresentation\"><Key name=\"SignatureAlgorithm\" /><Key name=\"EncryptAlgorithm\" /><Key name=\"CompressAlgorithm\" /></Group></RequestContext>");
            //base64 encode
            byte[] key_bytes = Encoding.Default.GetBytes(xml_key.ToString());
            byte[] order_bytes = Encoding.GetEncoding("UTF-8").GetBytes(order_id.ToString());
            byte[] result = new byte[1000];
            //string key_str_base64 = Convert.ToBase64String(key_bytes);
            //post to webservice
            com.drugadmin.sp.SuperPass ws = new gsp_nbr_verify.com.drugadmin.sp.SuperPass();
            ws.service("piats.superpass.bill.QueryChkInOutListsService", key_bytes, order_bytes, out result);
            //DataSet ds=GetDataSetByXml(Encoding.GetEncoding("UTF-8").GetString(result));
            DataSet ds = XmlDatasetConvert.ConvertXMLToDataSet(Encoding.GetEncoding("UTF-8").GetString(result));
            if (ds.Tables.Count != 0)
            {
                return ds.Tables[1];
            }
            else
            {
                return null;
            }
        }

        public static DataTable SPP_list_gsp_nbr(string usb_key, string asn_nbr, string asn_order)
        {
            DataTable gsp_dt = new DataTable();
            gsp_dt.Columns.Add("batch_nbr");
            gsp_dt.Columns.Add("gsp_nbr");
            string batch_nbr = "";
            string gsp_nbr = "";
            
            StringBuilder order_id = new StringBuilder();
            order_id.Append("<?xml version=\"1.0\"?><QueryChkInInfoRequest xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><storeInoutSeqNo>");
            order_id.Append(asn_order);
            order_id.Append("</storeInoutSeqNo><entStoreInId>");
            order_id.Append(asn_nbr);
            order_id.Append("</entStoreInId></QueryChkInInfoRequest>");
            StringBuilder xml_key = new StringBuilder();
            xml_key.Append("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><RequestContext><Group name=\"SystemInfo\"><Key name=\"SessionId\">");
            //xml_key.Append(richTextBox1.Text.Substring(3,32));
            xml_key.Append(usb_key);

            xml_key.Append("</Key></Group><Group name=\"DataPresentation\"><Key name=\"SignatureAlgorithm\" /><Key name=\"EncryptAlgorithm\" /><Key name=\"CompressAlgorithm\" /></Group></RequestContext>");
            //base64 encode
            byte[] key_bytes = Encoding.Default.GetBytes(xml_key.ToString());
            byte[] order_bytes = Encoding.GetEncoding("UTF-8").GetBytes(order_id.ToString());
            byte[] result = new byte[1000];
            com.drugadmin.sp.SuperPass ws = new gsp_nbr_verify.com.drugadmin.sp.SuperPass();
            ws.service("piats.superpass.bill.QueryChkInInfoService", key_bytes, order_bytes, out result);
            
            XmlDocument xml_doc = new XmlDocument();
            try
            {
                xml_doc.LoadXml(Encoding.GetEncoding("UTF-8").GetString(result));
            }
            catch (Exception ex)
            {
                gsp_dt.Rows.Add("warning:", ex.ToString());
            }

            XmlNodeList info_nl = xml_doc.SelectNodes("//QueryChkInInfoResponse/ChkInPhysicInfos/ChkInPhysicInfo");
            foreach (XmlNode info_node in info_nl)
            {
                batch_nbr = info_node.SelectSingleNode("produceBatchNo").InnerText.ToString();
                XmlNodeList gsp_nl = info_node.SelectNodes("codLists");
                foreach (XmlNode gsp_node in gsp_nl)
                {

                    gsp_nbr = gsp_node.SelectSingleNode("code").InnerText.ToString();
                    gsp_dt.Rows.Add(batch_nbr, gsp_nbr);
                }

            }

            return gsp_dt;

        }
    }
}
