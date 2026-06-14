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
            DatabaseDataContext db = new DatabaseDataContext();
            List<tbl_sinhvien> dSSV = db.tbl_sinhviens.ToList();
            dataGridView1.DataSource = dSSV;
            
           
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) {
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
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa");
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
    }
}
