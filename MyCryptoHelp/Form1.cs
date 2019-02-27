using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;


namespace MyCryptoHelp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }
        //string myPassword = "TEST_PASSWORD_~!@#";
        //string myPlainFile = "test.txt";
        //string myEncryptedFile = "test.encrypted";
        //string myDecryptedFile = "test.decrypted";


        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Do_Click(object sender, EventArgs e)
        {
            
            String s = "";
            try
            {
                if (this.openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //this.OpenFileDialog.Text = OpenFileDialog.FileName;
                    s = this.openFileDialog.FileName;
                }
                String pwd = this.pwd.Text;
                CryptoHelp.EncryptFile(s, s+".yh", pwd);
                MessageBox.Show("加密成功!\n加密后的文件为"+s + ".yh");
            }
            catch {
                //MessageBox.Show("请选择文件！");
            }
                
            
        }

        private void Undo_Click(object sender, EventArgs e)
        {
                String s1 = "";
            try
            {

                if (this.openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    s1 = this.openFileDialog.FileName;
                }
                try
                {
                    CryptoHelp.DecryptFile(s1, s1.Replace(".yh", ""), this.pwd.Text);
                    MessageBox.Show("解密成功!\n解密后的文件为"+s1.Replace(".yh",""));
                }
                catch
                {

                }
            }
            catch {
                //MessageBox.Show("请选择文件！");
            }

        }

        private void StrEncrypt_Click(object sender, EventArgs e)
        {
            string str = CryptoHelp.EncryptString(this.StrText.Text);
            MessageBox.Show("加密后的文本为：\n" + str);
            this.uString.Text = str;
        }

        private void StrrDecrypt_Click(object sender, EventArgs e)
        {

            string str = CryptoHelp.DecryptString(this.uString.Text);
            MessageBox.Show("解密后的文本为：\n" + str);
        }
    }
    /// <summary>
    /// CryptHelp
    /// </summary>
    public class CryptoHelp
 {
  private const ulong FC_TAG = 0xFC010203040506CF;

  private const int BUFFER_SIZE = 128*1024;
  
  /// <summary>
  /// 检验两个Byte数组是否相同
  /// </summary>
  /// <param name="b1">Byte数组</param>
  /// <param name="b2">Byte数组</param>
  /// <returns>true－相等</returns>
  private static bool CheckByteArrays(byte[] b1, byte[] b2)
  {
   if(b1.Length == b2.Length)
   {
    for(int i = 0; i < b1.Length; ++i)
    {
     if(b1[i] != b2[i])
      return false;
    }
    return true;
   }
   return false;
  }

  /// <summary>
  /// 创建Rijndael SymmetricAlgorithm
  /// </summary>
  /// <param name="password">密码</param>
  /// <param name="salt"></param>
  /// <returns>加密对象</returns>
  private static SymmetricAlgorithm CreateRijndael(string password, byte[] salt)
  {
   PasswordDeriveBytes pdb = new PasswordDeriveBytes(password,salt,"SHA256",1000);
   
   SymmetricAlgorithm sma = Rijndael.Create();
   sma.KeySize = 256;
   sma.Key = pdb.GetBytes(32);
   sma.Padding = PaddingMode.PKCS7;
   return sma;
  }

  /// <summary>
  /// 加密文件随机数生成
  /// </summary>
  private static RandomNumberGenerator rand = new RNGCryptoServiceProvider();

  /// <summary>
  /// 生成指定长度的随机Byte数组
  /// </summary>
  /// <param name="count">Byte数组长度</param>
  /// <returns>随机Byte数组</returns>
  private static byte[] GenerateRandomBytes(int count)
  {
   byte[] bytes = new byte[count];
   rand.GetBytes(bytes);
   return bytes;
  }

  /// <summary>
  /// 加密文件
  /// </summary>
  /// <param name="inFile">待加密文件</param>
  /// <param name="outFile">加密后输入文件</param>
  /// <param name="password">加密密码</param>
  public static void EncryptFile(string inFile, string outFile, string password)
  {
   using(FileStream fin = File.OpenRead(inFile),
       fout = File.OpenWrite(outFile))
   {
    long lSize = fin.Length; // 输入文件长度
    int size = (int)lSize;
    byte[] bytes = new byte[BUFFER_SIZE]; // 缓存
    int read = -1; // 输入文件读取数量
    int value = 0;
    
    // 获取IV和salt
    byte[] IV = GenerateRandomBytes(16);
    byte[] salt = GenerateRandomBytes(16);
    
    // 创建加密对象
    SymmetricAlgorithm sma = CryptoHelp.CreateRijndael(password, salt);
    sma.IV = IV;   
    
    // 在输出文件开始部分写入IV和salt
    fout.Write(IV,0,IV.Length);
    fout.Write(salt,0,salt.Length);
    
    // 创建散列加密
    HashAlgorithm hasher = SHA256.Create();
    using(CryptoStream cout = new CryptoStream(fout,sma.CreateEncryptor(),CryptoStreamMode.Write),
        chash = new CryptoStream(Stream.Null,hasher,CryptoStreamMode.Write))
    {
     BinaryWriter bw = new BinaryWriter(cout);
     bw.Write(lSize);
     
     bw.Write(FC_TAG);

     // 读写字节块到加密流缓冲区
     while( (read = fin.Read(bytes,0,bytes.Length)) != 0 )
     {
      cout.Write(bytes,0,read);
      chash.Write(bytes,0,read); 
      value += read;
     }
     // 关闭加密流

chash.Flush();
     chash.Close(); 


     // 读取散列
     byte[] hash = hasher.Hash;
     
     // 输入文件写入散列
     cout.Write(hash,0,hash.Length);

     // 关闭文件流
     cout.Flush();
     cout.Close();
    }
   }
  }

  /// <summary>
  /// 解密文件
  /// </summary>
  /// <param name="inFile">待解密文件</param>
  /// <param name="outFile">解密后输出文件</param>
  /// <param name="password">解密密码</param>
  public static Boolean DecryptFile(string inFile, string outFile, string password)
  {
   // 创建打开文件流
    Boolean result = true;
   using(FileStream fin = File.OpenRead(inFile),
       fout = File.OpenWrite(outFile))
   {
    int size = (int)fin.Length;
    byte[] bytes = new byte[BUFFER_SIZE];
    int read = -1;
    int value = 0;
    int outValue = 0;

    byte[] IV = new byte[16];
    fin.Read(IV,0,16);
    byte[] salt = new byte[16];
    fin.Read(salt,0,16);
    
    SymmetricAlgorithm sma = CryptoHelp.CreateRijndael(password,salt);
    sma.IV = IV;

    value = 32;
    long lSize = -1;
    
    // 创建散列对象, 校验文件
    HashAlgorithm hasher = SHA256.Create();

    using(CryptoStream cin = new CryptoStream(fin,sma.CreateDecryptor(),CryptoStreamMode.Read),
        chash = new CryptoStream(Stream.Null,hasher,CryptoStreamMode.Write))
    {
     // 读取文件长度
     BinaryReader br = new BinaryReader(cin);
     lSize = br.ReadInt64();
     ulong tag = br.ReadUInt64();

     if (FC_TAG != tag)
     {
         MessageBox.Show("文件被破坏");
         result = false;
     }
     long numReads = lSize / BUFFER_SIZE;

     long slack = (long)lSize % BUFFER_SIZE;
     
     for(int i = 0; i < numReads; ++i)
     {
      read = cin.Read(bytes,0,bytes.Length);
      fout.Write(bytes,0,read);
      chash.Write(bytes,0,read);
      value += read;
      outValue += read;
     }

     if(slack > 0)
     {
      read = cin.Read(bytes,0,(int)slack);
      fout.Write(bytes,0,read);
      chash.Write(bytes,0,read);
      value += read;
      outValue += read;
     }

     chash.Flush();
     chash.Close();

     fout.Flush();
     fout.Close();

     byte[] curHash = hasher.Hash;

     // 获取比较和旧的散列对象
     byte[] oldHash = new byte[hasher.HashSize / 8];
     read = cin.Read(oldHash,0,oldHash.Length);
     if((oldHash.Length != read) || (!CheckByteArrays(oldHash,curHash)))
      MessageBox.Show("文件被破坏");
     result = false;
    }

    if (outValue != lSize)
    {
        MessageBox.Show("文件大小不匹配");
        result = false;
    }
    return result;
   }
  }

        #region 字符串加密和解密
        static Byte[] Iv64 = { 22, 27, 31, 53, 53, 66, 97, 33 };
        static Byte[] byKye64 = { 79, 27, 57, 71, 89, 97, 32, 63 };
        ///<summary>
        ///字符串加密
        ///</summary>
        ///<param name="strText">要加密的字符串</param>
        ///<returns></returns>
        public static string EncryptString(string strText)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = System.Text.Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKye64, Iv64), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        ///<summary>
        ///字符串解密
        ///</summary>
        ///<param name="strText">要解密的字符串</param>
        ///<returns></returns>
        public static string DecryptString(string strText)
        {
            Byte[] inputByteArry = new Byte[strText.Length];
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArry = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKye64, Iv64), CryptoStreamMode.Write);
                cs.Write(inputByteArry, 0, inputByteArry.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}
