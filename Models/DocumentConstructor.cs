using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class DocumentConstructor 
    {
        public int DocumentConstructorId { get; set; }
      
        // Заголовок и Подвал
        public string Header { get; set; }
        public string Bottom { get; set; }
        public ICollection<DocumentConstructorLeftData> DocumentConstructorLeftDatas { get; set; }
        public ICollection<DocumentConstructorCenterData> DocumentConstructorCenterDatas { get; set; }
 
    }
    public class DocumentConstructorLeftData
    {
        public int DocumentConstructorLeftDataId { get; set; }
        // Имя тайтла
        public string Title { get; set; }
        public int Npp { get; set; }
        public int? SizeTitle { get; set; }
        public int DocumentConstructorId { get; set; }
        public virtual DocumentConstructor DocumentConstructor { get; set; }
        public virtual ICollection<DocumentConstructorCenterData> listBlocks { get; set; }

     
    }
    public class DocumentConstructorCenterData
    {
        public int DocumentConstructorCenterDataId { get; set; }
        // Хранит текст контента
        public string Content { get; set; }

        // Храню изображения base64=>byte[] и обратно также вывожу.
        public byte[] ImageData { get; set; }
        public int DocumentConstructorLeftDataId { get; set; }
        public virtual DocumentConstructorLeftData DocumentConstructorLeftData { get; set; }
        public virtual DocumentConstructor DocumentConstructor { get; set; }
    }


  

}