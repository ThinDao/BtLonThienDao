using BTLon_ThienDao.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BTLon_ThienDao.Models
{
    public class BaiDang
    {
        [Key]
        public int BaiID { get; set; }
        [Required]
        public int AccID { get; set; }
        [ForeignKey("AccID")]
        public Account? Account { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phai nhap ten cong ty!")]
        public string CongTy { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Phai nhap ngay dang")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayDang { get; set; }
        public int MucLuong { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phai nhap noi dung bai dang!")]
        [StringLength(4000, MinimumLength = 10, ErrorMessage = "Noi dung phai tu {2} den {1} ki tu")]
        public string NoiDung { get; set; }


        public ICollection<BaiDang>? DanhSachBaiDang { get; set; }
        [NotMapped]
        public int? SoLuongBaiDang { get; set; }
    }
}
