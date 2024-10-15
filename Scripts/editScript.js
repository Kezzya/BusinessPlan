
// Задаю id главного документа, по идее он всего 1 должен быть в программе...
const DocumentConstructorId = 4;
const $dropArea = $('.drop-area');

$dropArea.on('dragover', function (event) {
    event.preventDefault();
    $dropArea.addClass('highlight');
});

$dropArea.on('dragleave', function () {
    $dropArea.removeClass('highlight');
});

$dropArea.on('drop', function (event) {
    event.preventDefault();
    $dropArea.removeClass('highlight');
    const files = event.originalEvent.dataTransfer.files;
    const parentId = $(this).data("parentid");
    const previewId = parentId;
  
    handleFiles(files, parentId, previewId);
});

$('.fileElem').on('change', function (event) {
    const files = event.target.files;
    const parentId = $(this).data("parentid");
    const previewId = parentId;
    handleFiles(files, parentId, previewId);
});
function removeDataPrefix(dataUrl) {
    const regex = /^data:[^;]+;base64,/;
    return dataUrl.replace(regex, '');
}

function handleFiles(files, parentId, previewId) {
    if (files.length > 0) {
        const file = files[0];
        const reader = new FileReader();
        reader.onload = function (event) {
           
            const img = $('<img>').attr('src', event.target.result).css('max-width', '100%');
            $('#preview' + parentId).empty().append(img); // Очистить предыдущий просмотр и добавить новое изображение 
     
            const data = {
                "DocumentConstructorLeftDataId": parentId,
                "DocumentConstructorId": DocumentConstructorId,
                "ImageData": removeDataPrefix(event.target.result),
            }
            // Добавляем кнопку "Сохранить"
            const $saveButton = $('<button>')
                .text('Сохранить')
                .attr('id', 'save-button')
                .on('click', function () {
                    $.ajax({
                        url: '/DocumentConstructorCenterDatas/Create',
                        type: 'POST',
                        data: data,
                        
                        success: function (response) {
                            alert('Файл успешно сохранен!');
                        },
                        error: function (xhr, status, error) {
                            alert('Ошибка при сохранении файла: ' + error);
                        }
                    });
                });

            $('#preview' + parentId).append($saveButton); // Добавляем кнопку под изображением
        };
        reader.readAsDataURL(file);
    }
}
$(".fa-file-word").off("click", function () { }).on("click", function () {
    location.href = "/DocumentConstructors/GenerateDocument/" + DocumentConstructorId;

})
$("#saveHeaderTitle").off("click", function () { }).on("click", function () {
    let Title = $("#inputHeaderTitle").val();
    postHeadingTitle(Title);
    $("#headerTitleDialog").hide();
})
$("#saveBottomTitle").off("click", function () { }).on("click", function () {
    let Title = $("#inputBottomTitle").val();
    postBottomTitle(Title);
    $("#bottomTitleDialog").hide();
} )
function handlerInListButtons(id) {
    $("#list-buttons" +id).show();
}
function handlerOutListButtons(id) {
    $("#list-buttons" + id).hide();
}
function handleDialog() {
    $("#headerTitleDialog").show();
}
function handleDialogBottom() {
    $("#bottomTitleDialog").show();
}
//$(".top-title").on("click", postHeadingTitle);

function postBottomTitle(BottomTitle) {

    $.ajax({
        url: "/DocumentConstructors/Edit/" + DocumentConstructorId,
        type: 'Post',
        data: {
            "DocumentConstructorId": DocumentConstructorId,
            "Bottom": BottomTitle,
            "Header": $(".headerTitleSpan").text(),
             
           
        },
        success: function (success) {
      
        },
        error: function (error) {
            console.log(error)
        }
    })
}
function postHeadingTitle(Title) {

    $.ajax({
        url: "/DocumentConstructors/Edit/1",
        type: 'Post',
        data: {
            "DocumentConstructorId": DocumentConstructorId,
            "Header": Title,
            "Bottom": $(".bottomTitleSpan").text()
        },
        success: function (success) {
           
        },
        error: function (error) {
            console.log(error)
        }
    })
}
function getIdAndInsertText(start, end, textarea) {
   $.ajax({
        url: "/AccountOutTypes/GetId",
        type: 'GET',
        success: function (response) {
            myVariable = response.value;
            insertDataInTextArea(myVariable, start, end, textarea);           
            ($(textarea)[0]).selectionStart = start;
            ($(textarea)[0]).selectionEnd = end;
        },
        error: function
            (error) {
            console.log(error);
            }
    })
}

function sendDataContextMenu() {
    const contextMenu = $('.context-menu');
    const contextMenuInput = $("#contextMenuInput");
    let valueInput = contextMenuInput.val();
    contextMenu.css("display", 'none');
    $.ajax({
        url: "/AccountOutTypes/Create",
        type: 'Post',
        data: {
            "Title": valueInput,
    
        },
        success: function (response) {
            
        },
        error: function (error) {
            console.log(error);
        }
    })

}


function insertDataInTextArea(insertData, start, end, textarea) {
    // Вставляем текст
    const before = $(textarea).val().substring(0, start);
    const after = $(textarea).val().substring(end);
    $(textarea).val(before + `{`+ insertData + `}` + after);
}
function calcPosition(id) {
    event.preventDefault();
    let textAreaName = "#textAreaEdit" + id;
    let start = $(textAreaName).get(0).selectionStart;
    let end = $(textAreaName).get(0).selectionEnd;

    const contextMenu = $('.context-menu');

    // Устанавливаем позицию кастомного меню
    contextMenu.css("left", event.pageX + 'px' )
    contextMenu.css("top", event.pageY + 100 + 'px') 
    contextMenu.css("display", "block"); // Показываем кастомное меню
    
    // Скрываем кастомное меню при клике в любом месте
    $(".contextMenuButton").off("click").on("click",

        function () {
            sendDataContextMenu();
            getIdAndInsertText(start, end, textAreaName);
            ($(textAreaName)[0]).selectionStart = start;
            ($(textAreaName)[0]).selectionEnd = end;
            contextMenu.css("display", 'none');
 
        }

    );
    
}
//const textarea = $('#textAreaEdit2');
//const output = alert('output');
//$(textarea).
//textarea.addEventListener('mouseup', function () {
//    const start = textarea.selectionStart;
//    const end = textarea.selectionEnd;

//    // Получаем выделенный текст
//    const selectedText = textarea.value.substring(start, end);

//    // Получаем номер строки
//    const lines = textarea.value.substr(0, start).split('\n');
//    const lineNumber = lines.length; // Количество строк до начала выделения

//    // Получаем координаты выделенной строки
//    const lineStartIndex = lines[lineNumber - 1].length - (start - (textarea.value.lastIndexOf('\n', start - 1) + 1));

//    // Выводим координаты
//    output.innerHTML =
//        <strong>Выделенный текст:</strong> ${ selectedText } <br>
//            <strong>Номер строки:</strong> ${lineNumber}<br>
//                <strong>Координаты выделенной строки:</strong> (Строка: ${lineNumber}, Символ: ${lineStartIndex})
//                ;
//    });



//const regex = new RegExp("\{(.*?)\}", `g`);
//const text = $(".center-content").text();
//let highlightedText = text.replace(regex, '<span class="highlighted">$1</span>');

//console.log(highlightedText);
////while ((match = regex.exec(text)) !== null) {
////    const lineNumber = text.substring(0, match.index).split('\n').length; // Номер строки
////    const charIndex = match.index - text.lastIndexOf('\n', match.index) - 1; // Позиция в строке

////    console.log(`Найдено слово "${match[0]}" на строке ${lineNumber}, позиция ${charIndex}`);

   
////}

//function searchWordsRegex() {
//    while ((match = regex.exec(text)) !== null) {
//        const lineNumber = text.substring(0, match.index).split('\n').length; // Номер строки
//        const charIndex = match.index - text.lastIndexOf('\n', match.index) - 1; // Позиция в строке

//        console.log(`Найдено слово "${match[0]}" на строке ${lineNumber}, позиция ${charIndex}`);

//        // Обернём текст в HTML
//        let highlightedText = text.replace(regex, '<span class="highlighted">$1</span>');

//        console.log(highlightedText);
//        //// Вставим текст в элемент на странице
//        //document.getElementById('text-container').innerHTML = highlightedText;

//        //// Добавим обработчик события на все элементы с классом "highlighted"
//        //document.querySelectorAll('.highlighted').forEach(element => {
//        //    element.addEventListener('contextmenu', (event) => {
//        //        event.preventDefault(); // Отменяем стандартное контекстное меню
//        //        alert(Вы нажали правой кнопкой мыши на слово: "${element.textContent}");
//        //    });
//        //});

//    }
//}
function handleEdit(id) {
    classId = "." + id;
    $(classId).toggle();

    $('.saveEdit').click(function () {
        POSTEditLeftData(id)
    }
    )
}
$(`.SizeTitleUp`).click(
    function (e) {
        e.preventDefault();

        let itemId = $(this).attr('data-id');
        let url = `/DocumentConstructorLeftDatas/ChangeSizeTitle/` + itemId + `?changeNumber=-1`;
        $.ajax({
            url: url,
            type: 'GET',
            success: function (response) {
                location.reload();
            }
        });

    }
);

$(`.SizeTitleDown`).click(
    function (e) {
        e.preventDefault();

        let itemId = $(this).attr('data-id');
        let url = `/DocumentConstructorLeftDatas/ChangeSizeTitle/` + itemId + `?changeNumber=1`;
        $.ajax({

            url: url,
            type: 'GET',
            success: function (response) {
                location.reload();
            }
        });

    }
);

$('.NppUp').click(function (e) {
    e.preventDefault();
    let itemId = $(this).attr('data-id');
    $.ajax({
        url: '/DocumentConstructorLeftDatas/ChangeNppUp/' + itemId,
        type: 'GET',

        success: function (response) {

            location.reload(); // Перезагрузить страницу для обновления порядка

        }
    });
});

$('.NppDown').click(function (e) {
    e.preventDefault();
    let itemId = $(this).attr('data-id');
    $.ajax({
        url: '/DocumentConstructorLeftDatas/ChangeNppDown/' + itemId,
        type: 'GET',

        success: function (response) {

            location.reload(); // Перезагрузить страницу для обновления порядка

        }
    });

});
function GETLeftData() {
    $.ajax({
        url: '/DocumentConstructorLeftDatas/ChangeNpp/' + itemId,
        type: 'GET',
        success: function (response) {
            console.log('Успех');
            location.reload();
        },
        error: function (xhr, status, error) {
            console.error('Ошибка:', error);
        }

    })
}

function POSTDeleteLeftData(id) {

    if (confirm("Вы действительно хотите удалить?")) {

    }
    else {
        return false;
    }
    $.ajax({
        url: '/DocumentConstructorLeftDatas/Delete/' + id,
        type: 'Post',
        success: function (response) {

            let urlToRedirect = '/Home/Edit';
            location.relod();
        },
        error: function (xhr, status, error) {
            console.error('Ошибка:', id);
        }
    });
}
function POSTCreateLeftData() {
  
    $.ajax({
        url: '/DocumentConstructorLeftDatas/Create',
        type: 'POST',
        data: {
            "Title": $("#leftContentInput").val(),
            "Npp": 1,
            "SizeTitle": 4,
            "DocumentConstructorId": DocumentConstructorId
        },
        success: function (response) {
    
            location.reload();
        },
        error: function (xhr, status, error) {
            console.error('Ошибка:', error);
        }

    });
}
function POSTEditLeftData(id) {

    let data = {
        "DocumentConstructorId": DocumentConstructorId,
        "DocumentConstructorLeftDataId": id,
        "Title": $("#" + id).val(),
        "SizeTitle": 5,
        
    };
    $.ajax({
        url: '/DocumentConstructorLeftDatas/Edit/' + id,
        type: 'Post',
        data: data,
        success: function (response) {
    
            $(`.` + id).text(data.Title);
        },
        error: function (xhr, status, error) {
            console.error('Ошибка:', id);
        }

    });
}
$('.add').click(function () {
    $('.box').css("display", "block");
    $('.list-buttons').css("display", "none");
    $('#saveInput').click(
        POSTCreateLeftData
    );
});

function editContent(id) {

     
    $.ajax({
        url: '/DocumentConstructorLeftDatas/Edit/' + id,
        type: 'Post',
        data: data,
        success: function (response) {
            $(`.` + id).text(data.Title);
        },
        error: function (xhr, status, error) {
            console.error('Ошибка:', id);
        }
    })
}

function showTextArea(id) {
     
    let textAreaName = `#textArea` + id;
    let saveButtonName = `#saveContent` + id;
    let addContentName = "#addContent" + id;
    $(textAreaName).toggle();
    $(saveButtonName).toggle();
    $(addContentName).toggle();
}
function saveTextAreaValue(id) {
    let textAreaName = `#textArea` + id;
    let textAreaValue = $(textAreaName).val();
}
function addContent(id) {
    let saveButtonName = `#saveContent` + id;
    let textAreaName = `#textArea` + id;
    let addContentName = "#addContent" + id;
    $(addContentName).toggle();
    $(saveButtonName).toggle();
    let data = { "DocumentConstructorLeftDataId" : id ,
        "Content": $(textAreaName).val(),
        "DocumentConstructorId": DocumentConstructorId
    }
    $(textAreaName).toggle();
    saveTextAreaValue(id);
    $.ajax({
        url: '/DocumentConstructorCenterDatas/Create',
        type: 'Post',
        data: data,
        success: function (response) {
            location.reload();
            $(`.` + id).text(data.Title);
        },
        error: function (xhr, status, error) {
            console.error('Ошибка:', id);
        }
    })
}
function deleteContent(id) {
    
    if (confirm("Вы точно хотите удалить этот контент?")) {
        $.ajax({
            url: '/DocumentConstructorCenterDatas/Delete/' + id,
            type: 'Post',
            success: function (response) {
          
                location.reload();
            },
            error: function (xhr, status, error) {
                console.error('Ошибка:', id);
            }
        })
    }
}
function saveEditContent(id, parentId) {
    let textAreaName = "#textAreaEdit" + id;
    let editContentDivName = "#editContentDiv" + id;
    let saveContentName = "#saveContent" + id;
    let valueTextArea = $(textAreaName).val();
   
    $.ajax({
        url: '/DocumentConstructorCenterDatas/Edit/' + id,
        type: 'Post',
        data: {
            "DocumentConstructorCenterDataId": id,
            "DocumentConstructorLeftDataId": parentId,
            "Content": valueTextArea,
            "DocumentConstructorId": DocumentConstructorId
        },
        success: function (response) {
    
            $(saveContentName).toggle();
            $(editContentDivName).toggle();
            $(textAreaName).toggle();

        },
        error: function (xhr, status, error) {
            console.error('Ошибка:', id);
        }
    })
}
function editContent(id) {
    let textAreaName = "#textAreaEdit" + id;
    let editContentDivName = "#editContentDiv" + id;
    let saveContentName = "#saveContent" + id;
    $(textAreaName).toggle();

    let textDiv = $(editContentDivName).text();
    $(editContentDivName).toggle();
    $(saveContentName).toggle();
    $(textAreaName).val(textDiv);
    
    
}


