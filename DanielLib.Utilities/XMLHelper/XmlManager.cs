using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielLib.Utilities.XMLHelper
{
    public class XmlManager
    {
        public string xmlFilePath
        {
            private set;
            get;
        }

        public string xmlRootNode
        {
            private set;
            get;
        }

        /// <summary>
        /// 构造函数
        /// XmlManager xmlM = new XmlManager("Mail.xml", "/Root/MailList");
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="rootNode"></param>
        public XmlManager(string filePath, string rootNode)
        {
            this.xmlFilePath = filePath;
            this.xmlRootNode = rootNode;
        }

        /// <summary>
        /// 添加条目
        /// Dictionary<string, string> dictionary = new Dictionary<string, string>();
        /// dictionary.Add("id", "2");
        /// dictionary.Add("mail", "leowangzi@163.com");
        /// xmlM.AddItem("/Root/MailList", "Item", dictionary);
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="itemNode"></param>
        /// <param name="attributeIists"></param>
        public void AddItem(string parentNode, string itemNode, Dictionary<string, string> attributeIists)
        {
            bool _isFirst = true;
            string _id = "";
            string _val = "";

            foreach (var _item in attributeIists)
            {
                if (_isFirst)
                {
                    _id = _item.Key.ToString();
                    _val = _item.Value.ToString();
                    XmlHelper.Insert(this.xmlFilePath, parentNode, itemNode, _id, _val);
                    _isFirst = false;
                }
                else
                {
                    XmlHelper.Insert("Mail.xml", parentNode + "/" + itemNode + "[@" + _id + "='" + _val + "']", "", _item.Key.ToString(), _item.Value.ToString());
                } 
            }
        }

        /// <summary>
        /// 修改条目
        /// xmlM.UpdateItem("/Root/MailList", "Item", "id", "2", "mail", "amusegroup.com");
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="itemNode"></param>
        /// <param name="searchattributeinItem"></param>
        /// <param name="searchattributeinItemValue"></param>
        /// <param name="attributeinItem"></param>
        /// <param name="value"></param>
        public void UpdateItem(string parentNode, string itemNode, string searchattributeinItem, string searchattributeinItemValue, string attributeinItem, string value)
        {
            XmlHelper.Update(this.xmlFilePath, parentNode + "/" + itemNode + "[@" + searchattributeinItem + "='" + searchattributeinItemValue + "']", attributeinItem, value);
        }
    }
}
