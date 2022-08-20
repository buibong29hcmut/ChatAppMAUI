using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MaUI.Models
{
    public class BoxChatModel
    {
        public string UrlImage { get; set; }
        public string UserName { get; set; } = FactoryBoxChatProperty.Names[new Random().Next(0, FactoryBoxChatProperty.Names.Count())];
        public string Chat { get; set; } = "Hello, Ngày hôm ";
        public string DateTimeCreated { get; set; } = "6:30 PM";
    }
    public class FactoryBoxChatProperty
    {
        public static string[] Names = { "Bùi Bổng", "Huy Bùi", "Ngọc Bảo", "Đoàn Thế Đạt", "Thu Hà", "Thu Trà", "Ánh Ngọc", "Lê Hồng Phúc" };
       
    }
}
