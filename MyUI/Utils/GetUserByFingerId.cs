using System;
using System.Collections.Generic;
using System.Text;
using BLL = Gunark.BLL;
using Model = Gunark.Model;

namespace MyUI.Utils
{
    public class GetUserByFingerId
    {
        /// <summary>
        /// 根据指纹ID找到对应的警号
        /// </summary>
        /// <param name="id">指纹ID</param>
        /// <returns></returns>
        public static Model.fingerprint getUser(int id)
        {
            return new BLL.fingerprint().GetModel(id);
        }
    }
}
