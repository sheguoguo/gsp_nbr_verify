using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gsp_nbr_verify
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            richTextBox1.Text = webBrowser1.DocumentText;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(richTextBox1.Text!="")
            {
                string corp_id="<?xml version=\"1.0\"?><QueryOwnerCorpListRequest xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><entSeqNo>320000000000141855</entSeqNo></QueryOwnerCorpListRequest>";
                StringBuilder xml_key=new StringBuilder();
                xml_key.Append("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><RequestContext><Group name=\"SystemInfo\"><Key name=\"SessionId\">");
                //xml_key.Append(richTextBox1.Text.Substring(3,32));
                xml_key.Append("FCA75D6E3C7F53635B8EE46B3E641A52");
                xml_key.Append("</Key></Group><Group name=\"DataPresentation\"><Key name=\"SignatureAlgorithm\" /><Key name=\"EncryptAlgorithm\" /><Key name=\"CompressAlgorithm\" /></Group></RequestContext>");
                richTextBox2.Text = xml_key.ToString();
                //base64 encode
                byte[] key_bytes = Encoding.Default.GetBytes(xml_key.ToString());
                byte[] corp_bytes=Encoding.Default.GetBytes(corp_id);
                byte[] result=new byte[1000];
                //string key_str_base64 = Convert.ToBase64String(key_bytes);
                //post to webservice
                com.drugadmin.sp.SuperPass ws = new gsp_nbr_verify.com.drugadmin.sp.SuperPass();
                ws.service("piats.superpass.tpl.QueryOwnerCorpListService", key_bytes, corp_bytes,out result);
                richTextBox1.Text = Encoding.GetEncoding("UTF-8").GetString(result);
                

            }
        }
    }
}
