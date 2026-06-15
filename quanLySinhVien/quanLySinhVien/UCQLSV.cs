using QuanLySinhVien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login
{
    public partial class UCQLSV : UserControl
    {
        int tranghientai = 1;
        int sodongtrenmottrang = 10;
        int tongsotrang = 0;
        int tongsobanghi = 0;
        public UCQLSV()
        {
            InitializeComponent();
        }

        private void UCQLSV_Load(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            List<tbl_sinhvien> dSSV = db.tbl_sinhviens.ToList();
            dataGridView1.DataSource = dSSV;

            LoadComboBoxGioiTinh();
            LoadComboBoxLop();


        }
        public void LoadComboBoxGioiTinh()
        {
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");
            cboGioiTinh.SelectedIndex = -1;

        }
        public void LoadComboBoxLop()
        {
            DatabaseDataContext db = new DatabaseDataContext();
            List<tbl_lophoc> dSLop = db.tbl_lophocs.ToList();
            cboLop.DataSource = dSLop;
            cboLop.DisplayMember = "tenlop";
            cboLop.ValueMember = "malop";
            cboLop.SelectedIndex = -1;
        }

        private void btlThem_Click(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            tbl_sinhvien sv = new tbl_sinhvien();
            sv.masv = txtMaSV.Text;
            sv.hoten = txtHoTen.Text;
            sv.gioitinh = cboGioiTinh.SelectedItem.ToString();
            sv.ngaysinh = dtpNgaySinh.Value;
            sv.malop = cboLop.SelectedValue.ToString();
            db.tbl_sinhviens.InsertOnSubmit(sv);
            db.SubmitChanges();
            List<tbl_sinhvien> dSSV = db.tbl_sinhviens.ToList();
            dataGridView1.DataSource = dSSV;

            MessageBox.Show("Thêm sinh viên thành công");
        }

        private void cboGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            cboLop.SelectedIndex = -1;
            cboGioiTinh.SelectedIndex = -1;
            textBox5.Clear();
            DatabaseDataContext db = new DatabaseDataContext();
            List<tbl_sinhvien> dSSV = db.tbl_sinhviens.ToList();
            dataGridView1.DataSource = dSSV;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // lấy thông tin lên các ô input
                txtMaSV.Text = row.Cells["masv"].Value.ToString();
                txtHoTen.Text = row.Cells["hoten"].Value.ToString();
                cboGioiTinh.SelectedItem = row.Cells["gioitinh"].Value.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["ngaysinh"].Value);
                cboLop.SelectedValue = row.Cells["malop"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa");
                return;
            }
            string connectionString = "Data Source=alexxxx\\MSSQLSERVER04;Initial Catalog=qlsv;Persist Security Info=True;User ID=sa;Password=dream1012;TrustServerCertificate=True";
            string query = " UPDATE tbl_sinhviens SET hoten = @hoten, gioitinh=@gioitinh, ngaysinh = @ngaysinh, malop = @malop WHERE masv = @masv";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@masv", txtMaSV.Text);
                    cmd.Parameters.AddWithValue("@hoten", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@gioitinh", cboGioiTinh.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ngaysinh", dtpNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@malop", cboLop.SelectedValue.ToString());
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật sinh viên thành công");
                        // Cập nhật lại DataGridView
                        DatabaseDataContext db = new DatabaseDataContext();
                        List<tbl_sinhvien> dSSV = db.tbl_sinhviens.ToList();
                        dataGridView1.DataSource = dSSV;
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật sinh viên thất bại");
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa");
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string connectionString = "Data Source=alexxxx\\MSSQLSERVER04;Initial Catalog=qlsv;Persist Security Info=True;User ID=sa;Password=dream1012;TrustServerCertificate=True";
                string slq_xoa = "DELETE FROM tbl_sinhviens WHERE masv = @masv";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(slq_xoa, conn))
                    {
                        cmd.Parameters.AddWithValue("@masv", txtMaSV.Text);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa sinh viên thành công");
                            // Cập nhật lại DataGridView
                            DatabaseDataContext db = new DatabaseDataContext();
                            List<tbl_sinhvien> dSSV = db.tbl_sinhviens.ToList();
                            dataGridView1.DataSource = dSSV;
                        }
                        else
                        {
                            MessageBox.Show("Xóa sinh viên thất bại");
                        }
                    }
                }

            }
        }

        public void LoadDuLieu(string key = "")
        {
            using (DatabaseDataContext db = new DatabaseDataContext())
            {
                
                // Lay toan bo sinh vien
                var query = db.tbl_sinhviens.AsQueryable();

                // LOC SU LIEU QU NUT TIM KIEM
                if (!string.IsNullOrEmpty(key))
                {
                    query = query.Where(sv => sv.masv.Trim().Contains(key)
                                              || sv.hoten.Trim().Contains(key)
                                              || sv.malop.Trim().Contains(key));
                }
                tongsobanghi = query.Count();
                tongsotrang = (int)Math.Ceiling((double)tongsobanghi / sodongtrenmottrang);

                if (tongsotrang == 0)
                {
                    tongsotrang = 1;
                }
                //Áp dụng kỹ thuật phân trang: Bỏ qua(Skip) các trang trước, lấy(Take) dữ liệu trang hiện tại

                var hienThiDuLieu = query.Skip((tranghientai - 1) * sodongtrenmottrang)
                                            .Take(sodongtrenmottrang)
                                            .Select(sv => new
                                            {
                                                sv.masv,
                                                sv.hoten,
                                                sv.ngaysinh,
                                                sv.gioitinh,
                                                sv.malop
                                            })
                                            .ToList();
                //Đổ lên DataGridView và cập nhật dòng Label báo số trang (Trang 1/1 | 3 bản ghi)
                dataGridView1.DataSource = hienThiDuLieu;

                label7.Text = $"Trang {tranghientai}/{tongsotrang} | {tongsobanghi} bản ghi";
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            tranghientai = 1;
            string key = textBox5.Text.Trim();
            LoadDuLieu(key);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (tranghientai > 1)
            {
                tranghientai--;
                string key = textBox5.Text.Trim();
                LoadDuLieu(key);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (tranghientai < tongsotrang)
            {
                tranghientai++;
                string key = textBox5.Text.Trim();
                LoadDuLieu(key);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            tranghientai = tongsotrang;
            string key = textBox5.Text.Trim();
            LoadDuLieu(key);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tranghientai = 1;
            string key = textBox5.Text.Trim();
            LoadDuLieu(key);
        }
    }
}
