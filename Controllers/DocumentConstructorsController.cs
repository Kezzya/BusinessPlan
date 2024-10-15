using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Diagnostics;
using AngleSharp.Dom;
using System.Xml.Linq;
using DocumentFormat.OpenXml;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using P = DocumentFormat.OpenXml.Drawing.Pictures;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
 

namespace WebApplication2.Controllers
{
    public class DocumentConstructorsController : Controller
    {
        private DocumentConstructorContext db = new DocumentConstructorContext();

        // GET: DocumentConstructors
        public ActionResult Index()
        {
            return View(db.DocumentConstructors.ToList());
        }

        // GET: DocumentConstructors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentConstructor documentConstructor = db.DocumentConstructors.Find(id);
            if (documentConstructor == null)
            {
                return HttpNotFound();
            }
            return View(documentConstructor);
        }

        // GET: DocumentConstructors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentConstructors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocumentConstructorId,Header,Bottom")] DocumentConstructor documentConstructor)
        {
            if (ModelState.IsValid)
            {
                db.DocumentConstructors.Add(documentConstructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentConstructor);
        }

        // GET: DocumentConstructors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentConstructor documentConstructor = db.DocumentConstructors.Find(id);
            if (documentConstructor == null)
            {
                return HttpNotFound();
            }
            return View(documentConstructor);
        }

        // POST: DocumentConstructors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
  
        public ActionResult Edit([Bind(Include = "DocumentConstructorId,Header,Bottom")] DocumentConstructor documentConstructor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentConstructor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentConstructor);
        }

        // GET: DocumentConstructors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentConstructor documentConstructor = db.DocumentConstructors.Find(id);
            if (documentConstructor == null)
            {
                return HttpNotFound();
            }
            return View(documentConstructor);
        }

        // POST: DocumentConstructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentConstructor documentConstructor = db.DocumentConstructors.Find(id);
            db.DocumentConstructors.Remove(documentConstructor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // /DocumentConstructors/GenerateDocument/4
        public ActionResult GenerateDocument(int documentId = 4)
        {
            var header = db.DocumentConstructors
            .Where(d => d.DocumentConstructorId == documentId)
            .Select(d => d.Header)
            .FirstOrDefault();

            var bottom = db.DocumentConstructors
           .Where(d => d.DocumentConstructorId == documentId)
           .Select(d => d.Bottom)
           .FirstOrDefault();

            var titles = db.DocumentConstructorLeftDatas.ToList()
                .Select(t => t.Title);
    

            string templatePath = Server.MapPath("~/Templates/Template.dotx");
            string outputPath = Server.MapPath("~/generated.docx");

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Копируем шаблон в новый документ
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(templatePath, true))
                {
                    // Создаем новый документ на основе шаблона
                    wordDoc.Clone(memoryStream);
                }

                using (WordprocessingDocument generatedDoc = WordprocessingDocument.Open(memoryStream, true))
                {

                    DocumentSettingsPart settingsPart = generatedDoc.MainDocumentPart.GetPartsOfType<DocumentSettingsPart>().First();
                    // Create object to update fields on open
                    UpdateFieldsOnOpen updateFields = new UpdateFieldsOnOpen();
                    updateFields.Val = new DocumentFormat.OpenXml.OnOffValue(true);
                    // Insert object into settings part.
                    settingsPart.Settings.PrependChild<UpdateFieldsOnOpen>(updateFields);
                    settingsPart.Settings.Save();
                    settingsPart.Settings.Append(new UpdateFieldsOnOpen() { Val = true });
                    // Добавляем main в документ
                    var mainDocumentPart = generatedDoc.MainDocumentPart;
                    // Добавляем body в документ
                    var body = generatedDoc.MainDocumentPart.Document.Body;

                    // Получаем часть документа
                    var headerParts = generatedDoc.MainDocumentPart.HeaderParts;
                    var footerParts = generatedDoc.MainDocumentPart.FooterParts;
                   
                    foreach (var parts in headerParts)
                    {
                        //Gets the text in headers
                        foreach (var currentText in parts.RootElement.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>())
                        {
                            currentText.Text = currentText.Text.Replace("Шапка", header);
                        }
                    }
                    foreach (var parts in footerParts)
                    {
                        //Gets the text in footers
                        foreach (var currentText in parts.RootElement.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>())
                        {
                            currentText.Text = currentText.Text.Replace("Подвал", bottom);
                        }
                    }

                    var listItems = db.DocumentConstructorLeftDatas.OrderBy(l => l.Npp).ToList();

                    ImagePart imgp = mainDocumentPart.AddImagePart(ImagePartType.Png);
                   
                    foreach (var item in listItems)
                    {
                        foreach (var list in item.listBlocks)
                        {
                            if(list.ImageData != null)
                            {
                                using (var imageStream = new MemoryStream(list.ImageData))
                                {
                                    imgp.FeedData(imageStream);
                                }

                                AddImageToBody(mainDocumentPart, list.ImageData);
                            }
                            if(list.Content != null)
                            {
                                AddHeading(body, item.Title, 1, mainDocumentPart.StyleDefinitionsPart);
                            }
                            
                            ReplaceText(body, "Заголовок", "");
                            Paragraph para = body.AppendChild(new Paragraph());
                            Run run = para.AppendChild(new Run(new Text(list.Content)));
                         

                            ReplaceText(body, "КонтентЗаголовка","");
                          
                        }

                    }
                    generatedDoc.Save();
                    // Сохраняем документ
                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create))
                    {
                        memoryStream.WriteTo(fileStream);
                    }
                }
            }
            
            return File(outputPath, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "generated.docx");
        }
        private void AddHeading(Body body, string text, int level, StyleDefinitionsPart styleDefinitionsPart)
        {
            // Создаем заголовок
            Paragraph heading = new Paragraph();
            Run run = new Run(new Text(text));

            string styleId = level == 1 ? "Heading1" : "Heading2";
            

            // Проверяем, существует ли стиль в StyleDefinitionsPart
            if (styleDefinitionsPart != null && styleDefinitionsPart.Styles.Elements<Style>().Any(s => s.StyleId == styleId))
            {
                heading.ParagraphProperties = new ParagraphProperties(new ParagraphStyleId() { Val = styleId });
            }
            heading.Append(run);
            body.Append(heading);
        }
        private void ReplaceText(Body body, string placeholder, string newValue)
        {
            // Ищем текст и заменяем его
            foreach (var text in body.Descendants<Text>())
            {
                if (text.Text.Contains(placeholder))
                {
                  text.Text = text.Text.Replace(placeholder, newValue);
                }
            }
        }
        private void AddImageToBody(MainDocumentPart mainPart, byte[] imageBytes)
        {
            // Добавляем изображение в документ
            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Png); // Укажите тип изображения
            using (MemoryStream stream = new MemoryStream(imageBytes))
            {
                imagePart.FeedData(stream);
            }

            // Создаем элемент для изображения
            var element = new Drawing(
                new DW.Inline(
                    new DW.Extent() { Cx = 4990000L, Cy = 2092000L }, // Укажите размеры изображения
                    new DW.EffectExtent() { LeftEdge = 0L, RightEdge = 0L, TopEdge = 0L, BottomEdge = 0L },
                    new DW.DocProperties() { Id = (UInt32Value)1U, Name = "Picture" },
                    new DW.NonVisualGraphicFrameDrawingProperties(
                        new A.GraphicFrameLocks() { NoChangeAspect = true }),
                    new A.Graphic(
                        new A.GraphicData(
                            new P.Picture(
                                new P.NonVisualPictureProperties(
                                    new P.NonVisualDrawingProperties() { Id = (UInt32Value)0U, Name = "Picture" },
                                    new P.NonVisualPictureDrawingProperties()),
                                new P.BlipFill(
                                    new A.Blip() { Embed = mainPart.GetIdOfPart(imagePart) },
                                    new A.Stretch(new A.FillRectangle())),
                                new P.ShapeProperties(
                                    new A.Transform2D(
                                        new A.Offset() { X = 0L, Y = 0L },
                                        new A.Extents() { Cx = 4990000L, Cy = 2092000L }),
                                    new A.PresetGeometry() { Preset = A.ShapeTypeValues.Rectangle }))
                            )
                        )
                    )
                );
            // Добавляем элемент в тело документа
            mainPart.Document.Body.AppendChild(new Paragraph(new Run(element)));
        }
         

    }
}
