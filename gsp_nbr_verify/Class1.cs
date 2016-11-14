// <summary>  
       /// 通过用户名和密码 返回下行数据  
       /// </summary>  
       /// <param name="UserName">用户名</param>  
       /// <param name="UserPwd">密码</param>  
       /// <returns></returns>  
       [WebMethod] 
       public XmlDataDocument GetUpMassageDate(string UserName, string UserPwd) 
       { 
           try
           { 
               XmlDataDocument xd = new XmlDataDocument(); 
               DataSet ds = DbHelperSQL.Query("select   Mobile,UPMessge, RecordDate from dbo.NA_Activity_Data where ActivityID in( select ActivityID from dbo.NA_Activity  where UserID in (select UserID from dbo.NA_User  where UserName='" + UserName.Trim() + "' and UserPwd='" + UserPwd.Trim() + "'))"); 
               if (ds != null && ds.Tables.Count > 0) 
               { 
                   xd = new XmlDataDocument(ds); 
                   XmlNode root1 = xd.DocumentElement; 
  
                   XmlNodeList roots = root1.SelectNodes("ds"); 
                   foreach (XmlNode item in roots) 
                   { 
                       XmlNodeList list = item.SelectNodes("RecordDate"); 
                       ds.EnforceConstraints = false;  //如果需要修改xml里的数据  需要加上这句  
                       foreach (XmlNode node in list) 
                       { 
                           //这里是修改XML中 RecordDate的时间格式 原始格式是：  <RecordDate>2012-04-20T16:16:00+08:00</RecordDate>   
                           node.InnerText = Convert.ToDateTime(node.InnerText.ToString()).ToString("yyyy-MM-dd HH:mm");   
                       } 
                   } 
                   return xd; 
               } 
               else
               { 
                   return null; 
               } 
           } 
           catch (Exception ex) 
           { 
               return null; 
           } 
       } 
 
 /// <summary>
        /// 通过用户名和密码 返回下行数据
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="UserPwd">密码</param>
        /// <returns></returns>
        [WebMethod]
        public XmlDataDocument GetUpMassageDate(string UserName, string UserPwd)
        {
            try
            {
                XmlDataDocument xd = new XmlDataDocument();
                DataSet ds = DbHelperSQL.Query("select   Mobile,UPMessge, RecordDate from dbo.NA_Activity_Data where ActivityID in( select ActivityID from dbo.NA_Activity  where UserID in (select UserID from dbo.NA_User  where UserName='" + UserName.Trim() + "' and UserPwd='" + UserPwd.Trim() + "'))");
                if (ds != null && ds.Tables.Count > 0)
                {
                    xd = new XmlDataDocument(ds);
                    XmlNode root1 = xd.DocumentElement;
 
                    XmlNodeList roots = root1.SelectNodes("ds");
                    foreach (XmlNode item in roots)
                    {
                        XmlNodeList list = item.SelectNodes("RecordDate");
                        ds.EnforceConstraints = false;  //如果需要修改xml里的数据  需要加上这句
                        foreach (XmlNode node in list)
                        {
                            //这里是修改XML中 RecordDate的时间格式 原始格式是：  <RecordDate>2012-04-20T16:16:00+08:00</RecordDate>
                            node.InnerText = Convert.ToDateTime(node.InnerText.ToString()).ToString("yyyy-MM-dd HH:mm"); 
                        }
                    }
                    return xd;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
