using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using Oracle.DataAccess.Client;


namespace gsp_nbr_verify
{
    public partial class compare_form : Form
    {
        public compare_form()
        {
            InitializeComponent();

            
        }

        private string shp_mnt_nbr="";
        DataTable gsp_online_dt = null;
        DataTable gsp_local_dt = null;
        string gsp_key = "4C10B363C125B6B0AF4DACADDC493E82";

        private void button1_Click(object sender, EventArgs e)
        {
            //DataTable owner_dt = SPPHelper.SPP_list_owner("FCA75D6E3C7F53635B8EE46B3E641A52");
            string order_nbr = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string owner_name = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            DataTable order_dt = SPPHelper.SPP_list_order(gsp_key, order_nbr, SPPHelper.owner_name_to_nbr(owner_name));
            if (order_dt != null)
            {
                dataGridView1.DataSource = order_dt;
                
            }
            else
            { 
                MessageBox.Show("药监网上未有此单信息，请确认是否已上传"); 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable owner_dt = SPPHelper.SPP_list_owner(gsp_key);
            //DataTable order_dt = SPPHelper.SPP_list_order("FCA75D6E3C7F53635B8EE46B3E641A52", "BTASNP1508030018082", "320000000000124343");
            dataGridView1.DataSource = owner_dt;
            foreach (DataRow dr in owner_dt.Rows)
            {
                comboBox1.Items.Add(dr[1]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection conn = oraclehelper.GetOracleConnectionAndOpen)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        string sql_text = "select ah.shpmt_nbr as 入库单号,substr(ah.shpmt_nbr,1,2) as 货主,ah.create_date_time as 创建时间,' ' as 是否上传, ' ' as 药监网编号,' ' as 监管码校验情况  from asn_hdr ah where ah.to_whse='S00' and ah.stat_code=90 and to_char(ah.create_date_time,'yyyymmdd') >=" + dateTimePicker1.Value.ToString("yyyyMMdd") + " and to_char(ah.create_date_time,'yyyymmdd')<=" + dateTimePicker2.Value.ToString("yyyyMMdd");
                        if (textBox1.Text != "")
                        {
                            sql_text = sql_text + " and substr(ah.shpmt_nbr,1,2)='" + textBox1.Text + "'";
                        }
                        dataGridView1.DataSource = oraclehelper.ExecuteDataTable(sql_text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常, 异常信息: " + ex.Message);
            } 
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            string asn_order = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string asn_nbr = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            shp_mnt_nbr = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            DataTable order_dt = SPPHelper.SPP_list_gsp_nbr(gsp_key, asn_nbr, asn_order);
            if (order_dt != null)
            {
                dataGridView1.DataSource = order_dt;
                gsp_online_dt = order_dt;

            }
            else
            {
                MessageBox.Show("药监网上未有此单信息，请确认是否已上传");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            DataTable gsp_local_dt = null;
            try
            {
                using (OracleConnection conn = oraclehelper.GetOracleConnectionAndOpen)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        string sql_text = "";
                        if (shp_mnt_nbr != "")
                        {
                            sql_text = "select c.batch_nbr as batch_nbr,c.gsp_nbr as gsp_nbr from c_gsp_nbr_trkg c where c.stat_code=0 and c.rcvd_shpmt_nbr='"+shp_mnt_nbr+"'";
                        }
                        dataGridView1.DataSource = null;
                        gsp_local_dt = oraclehelper.ExecuteDataTable(sql_text);
                        
                    }
                }
                IEnumerable<DataRow> query = gsp_online_dt.AsEnumerable().Except(gsp_local_dt.AsEnumerable(), DataRowComparer.Default);
                if (query.Count() != 0)
                    dataGridView1.DataSource = query.CopyToDataTable();
                    
                else
                    MessageBox.Show("不存在差异");
            }
                

            catch (Exception ex)
            {
                MessageBox.Show("出现异常, 异常信息: " + ex.Message);
            } 
            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string order_nbr = "";
            //string owner_name = "";
            DataTable order_dt = null;

            if (dataGridView1.Rows.Count!=0)
            {
                foreach (DataGridViewRow order_dgvr in dataGridView1.Rows)
                {
                    try
                    {
                         order_dt = SPPHelper.SPP_list_order(gsp_key, order_dgvr.Cells["入库单号"].Value.ToString(), SPPHelper.owner_name_to_nbr(order_dgvr.Cells["货主"].Value.ToString()));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("药监网登录超时，请退出后重新登录！");
                        return;
                    }
                    if (order_dt != null)
                    {
                        order_dgvr.Cells["是否上传"].Value = "已上传";
                        order_dgvr.Cells["药监网编号"].Value = order_dt.Rows[0][1].ToString();
                        dataGridView1.Refresh();
                        
                    }
                    else
                    {
                        order_dgvr.Cells["是否上传"].Value = "未上传";
                        order_dgvr.DefaultCellStyle.ForeColor = Color.Red;
                        dataGridView1.Refresh();
                    }
 
                }
            }

            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //this.UseWaitCursor = true;
            if (dataGridView1.Rows.Count != 0)
            {
                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Maximum = dataGridView1.Rows.Count;

                foreach (DataGridViewRow order_dgvr in dataGridView1.Rows)
                {
                    if (order_dgvr.Cells["是否上传"].Value.ToString() == "已上传" && order_dgvr.Cells["药监网编号"].Value.ToString() != "")
                    {


                        try
                        {
                            //获取药监网监管码批号表
                            gsp_online_dt = SPPHelper.SPP_list_gsp_nbr(gsp_key, order_dgvr.Cells["入库单号"].Value.ToString(), order_dgvr.Cells["药监网编号"].Value.ToString());

                            //查询数据库监管码批号表
                            using (OracleConnection conn = oraclehelper.GetOracleConnectionAndOpen)
                            {
                                if (conn.State == ConnectionState.Open)
                                {
                                    string sql_text = "select c.batch_nbr as batch_nbr,c.gsp_nbr as gsp_nbr from c_gsp_nbr_trkg c where c.stat_code=0 and c.rcvd_shpmt_nbr='" + order_dgvr.Cells["入库单号"].Value.ToString() + "'";


                                    gsp_local_dt = oraclehelper.ExecuteDataTable(sql_text);

                                }
                            }
                            IEnumerable<DataRow> query = gsp_online_dt.AsEnumerable().Except(gsp_local_dt.AsEnumerable(), DataRowComparer.Default);
                            if (query.Count() != 0)
                            {
                                order_dgvr.Cells["监管码校验情况"].Value = "共" + query.Count().ToString() + "条药监码批号校验有误";
                                order_dgvr.DefaultCellStyle.BackColor = Color.Red;
                                dataGridView1.Refresh();

                            }

                            else
                            {
                                order_dgvr.Cells["监管码校验情况"].Value = "全匹配";
                                dataGridView1.Refresh();
                            }
                        }


                        catch (Exception ex)
                        {
                            MessageBox.Show("出现异常, 异常信息: " + ex.Message);
                        }

                    }
                    else
                    {
                        MessageBox.Show("请先查询上传情况再进行校验");
                        return;
                    }

                    toolStripProgressBar1.Value += 1;
                }
 
            }
            //this.UseWaitCursor = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView2.DataSource != null)
            {
                DataTable dt = (DataTable)dataGridView2.DataSource;
                dt.Rows.Clear();
                dataGridView2.DataSource = dt;
            }
            if (dataGridView1.Rows.Count != 0)
            {
                if (dataGridView1.CurrentRow.Cells["监管码校验情况"].Value.ToString()!="")
                {

                    try
                    {
                        //获取药监网监管码批号表
                        gsp_online_dt = SPPHelper.SPP_list_gsp_nbr(gsp_key, dataGridView1.CurrentRow.Cells["入库单号"].Value.ToString(), dataGridView1.CurrentRow.Cells["药监网编号"].Value.ToString());

                        //查询数据库监管码批号表
                        using (OracleConnection conn = oraclehelper.GetOracleConnectionAndOpen)
                        {
                            if (conn.State == ConnectionState.Open)
                            {
                                string sql_text = "select c.batch_nbr as batch_nbr,c.gsp_nbr as gsp_nbr from c_gsp_nbr_trkg c where c.stat_code=0 and c.rcvd_shpmt_nbr='" + dataGridView1.CurrentRow.Cells["入库单号"].Value.ToString() + "'";


                                gsp_local_dt = oraclehelper.ExecuteDataTable(sql_text);

                            }
                            
                        }
                        IEnumerable<DataRow> query = gsp_online_dt.AsEnumerable().Except(gsp_local_dt.AsEnumerable(), DataRowComparer.Default);
                        if (query.Count() != 0)
                        {
                            dataGridView2.DataSource = query.CopyToDataTable();
                            
                                using (OracleConnection conn = oraclehelper.GetOracleConnectionAndOpen)
                                {
                                    if (conn.State == ConnectionState.Open)
                                    {
                                        foreach (DataGridViewRow temp_dgvr in dataGridView2.Rows)
                                        {
                                            string sql_text = "select ch.case_nbr,lh.locn_brcd locn_nbr from c_gsp_nbr_trkg c left join case_hdr ch on ch.case_nbr=c.cntr_nbr left join locn_hdr lh on lh.locn_id=ch.locn_id and lh.whse='S00' where c.gsp_nbr='"+temp_dgvr.Cells["监管码"].ToString()+"'";
                                            DataTable temp_dt = oraclehelper.ExecuteDataTable(sql_text);
                                            temp_dgvr.Cells["所在货箱号"].Value = temp_dt.Rows[0][0].ToString();
                                            temp_dgvr.Cells["所在库位号"].Value = temp_dt.Rows[0][1].ToString();
                                            dataGridView2.Refresh();
                                        }

                                    }

                                }
                            

                        }

                        else
                        {
                            MessageBox.Show("无异常信息");

                        }
                    }


                    catch (Exception ex)
                    {
                        MessageBox.Show("出现异常, 异常信息: " + ex.Message);
                    }
                }


                else
                {
                    MessageBox.Show("请先进行该单据的批号校验，谢谢！");
                    return;
                }
            }
        }

        private void compare_form_Load(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = DateTime.Now.ToString();
        } 


        
    }
}
