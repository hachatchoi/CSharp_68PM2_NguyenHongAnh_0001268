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
    public partial class UCQLLH : UserControl
    {
        int tranghientai = 1;
        int tongsotrang = 0;
        int tongsobanghi = 0;
        int sodongtrenmottrang = 5;

        public UCQLLH()
        {
            InitializeComponent();
        }

        private void UCQLLH_Load(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            List<tbl_lophoc> lHoc = db.tbl_lophocs.ToList();
            dataGridView1.DataSource = lHoc;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["id"].Value.ToString();
                textBox2.Text = row.Cells["malop"].Value.ToString();
                textBox3.Text = row.Cells["tenlop"].Value.ToString();
                textBox4.Text = row.Cells["ghichu"].Value.ToString();

                
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tranghientai = 1;
            string key = textBox5.Text.Trim();
            LoadDuLieu(key);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            tbl_lophoc lh = new tbl_lophoc();   
            lh.malop = textBox2.Text;
            lh.tenlop = textBox3.Text;
            lh.ghichu = textBox4.Text;
            db.tbl_lophocs.InsertOnSubmit(lh);
            db.SubmitChanges();
            UCQLLH_Load(sender, e);
            MessageBox.Show("Thêm thành công");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn một lớp học để sửa.");
                return;
            }
                string connectionString = "Data Source=alexxxx\\MSSQLSERVER04;Initial Catalog=qlsv;Persist Security Info=True;User ID=sa;Password=dream1012;TrustServerCertificate=True";
                string slq_sua = "UPDATE tbl_lophocs SET malop = @malop, tenlop = @tenlop, ghichu = @ghichu WHERE id = @id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(slq_sua, connection))
                    {
                        command.Parameters.AddWithValue("@id", textBox1.Text);
                        command.Parameters.AddWithValue("@malop", textBox2.Text);
                        command.Parameters.AddWithValue("@tenlop", textBox3.Text);
                        command.Parameters.AddWithValue("@ghichu", textBox4.Text);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sửa thành công");
                            UCQLLH_Load(sender, e);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Sửa thất bại. Vui lòng kiểm tra lại thông tin.");
                            return;
                        }
            }
        }
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn một lớp học để xóa.");
                return;
            }


            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa lớp học này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string connectionString = "Data Source=alexxxx\\MSSQLSERVER04;Initial Catalog=qlsv;Persist Security Info=True;User ID=sa;Password=dream1012;TrustServerCertificate=True";
                string slq_xoa = "DELETE FROM tbl_lophocs WHERE malop = @malop";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(slq_xoa, connection))
                    {
                        command.Parameters.AddWithValue("@malop", textBox2.Text);
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Xóa thành công");
                        UCQLLH_Load(sender, e);
                        return;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            UCQLLH_Load(sender, e);
            return;
        }
        public void LoadDuLieu(string key = "")
        {
            using (DatabaseDataContext db = new DatabaseDataContext())
            {

                // Lay toan bo sinh vien
                var query = db.tbl_lophocs.AsQueryable();

                // LOC SU LIEU QU NUT TIM KIEM
                if (!string.IsNullOrEmpty(key))
                {
                    query = query.Where(lh => lh.malop.Trim().Contains(key)
                                              || lh.tenlop.Trim().Contains(key));
                                              
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
                                            .Select(lh => new
                                            {
                                                lh.id,
                                                lh.malop,
                                                lh.tenlop,
                                                lh.ghichu
                                            })
                                            .ToList();
                //Đổ lên DataGridView và cập nhật dòng Label báo số trang (Trang 1/1 | 3 bản ghi)
                dataGridView1.DataSource = hienThiDuLieu;

                label7.Text = $"Trang {tranghientai}/{tongsotrang} | {tongsobanghi} bản ghi";
            }

        }
                                               
            }

        }
    


