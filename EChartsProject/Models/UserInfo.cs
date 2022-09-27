using System.Runtime.Serialization;

namespace EChartsProject.Models
{
    public class UserParams
    {
        public int id { get; set; }

        public string name { get; set; }

        public DateTime stTime { get; set; }

        public DateTime edTime { get; set; }

        public int page { get; set; }

        public int rows { get; set; }

        public bool isExport { get; set; }
    }

    [DataContract]
    public class UserInfo
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "pwd")]
        public string Pwd { get; set; }

        [DataMember(Name = "gender")]
        public string Gender { get; set; }

        [DataMember(Name = "tel")]
        public long Tel { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "createTime")]
        public string CreateTime { get; set; }

        [DataMember(Name = "picurl")]
        public string PicUrl { get; set; }
    }

    public class UserInfoList
    {
        public int total { get; set; }
        public List<UserInfo> rows { get; set; }
    }
}
