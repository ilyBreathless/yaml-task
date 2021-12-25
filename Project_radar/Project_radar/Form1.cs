using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
namespace Project_radar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string path = @"D:\user.yaml";

        List<User> listOfUsers = new List<User>()
        {
             new User() { firstName = "Ilya",surName ="Kaygorodov", Age = 20 },
             new User() { firstName = "Denis",surName ="Demenkovec", Age = 35 },
             new User() { firstName = "Andrey",surName = "Gavrik", Age = 20},
        };
        private void button1_Click(object sender, EventArgs e)
        {
            var serializer = new SerializerBuilder()
                  .WithNamingConvention(CamelCaseNamingConvention.Instance)
                  .Build();

            var stringResult = serializer.Serialize(listOfUsers);
            
            using (FileStream fs = new FileStream(path,FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(stringResult);
                fs.Write(array, 0, array.Length);
                label2.Text = "список объектов записан в файл user.yaml по адресу "  + path; 
            }
            
        }
        class User
        {
            public string firstName { get; set; }

            public string surName { get; set; }
            public int Age { get; set; }

            public override string ToString()
            {
                return this.firstName + " " + this.surName + " " + this.Age;
            }

        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
           var deserializer = new DeserializerBuilder()
              .WithNamingConvention(CamelCaseNamingConvention.Instance)  
              .Build();
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                var deserializeResult = deserializer.Deserialize<List<User>>(textFromFile);
               
                string combindedString = string.Join(",", deserializeResult);
                label4.Text = combindedString;
              
            }
        }

      
    }
}
