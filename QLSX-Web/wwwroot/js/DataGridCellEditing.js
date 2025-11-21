 
function onUpdatingTemplateColumnNhapXuat(grid, args) {
    let content = args.content;
    var row = args.cellInfo.dataRow;
    if (content.childElementCount === 0) {
        var deleteButton = document.createElement("button");
        deleteButton.id = row.id;
        deleteButton.innerHTML = " <igx-icon class='material-icons igx-icon' aria-hidden='true'>delete</igx-icon>"


        var createButton = document.createElement("button");
        createButton.id = row.id;
        createButton.innerHTML = "<igx-icon class='material-icons igx-icon' aria-hidden='true'>add</igx-icon>"


        var editButton = document.createElement("button");
        editButton.id = row.id;
        editButton.innerHTML = " <igx-icon class='material-icons igx-icon' aria-hidden='true'>edit</igx-icon>"

        var printButton = document.createElement("button");
        printButton.id = row.id;
        printButton.innerHTML = " <igx-icon class='material-icons igx-icon' aria-hidden='true'>print</igx-icon>"


        createButton.onclick = function () {
            var button = deleteButton;
            DotNet.invokeMethodAsync('QLSX.Web','ButtonClickActionAtBlazorLevel', button.id);
        }
        editButton.onclick = function () {
            var button = deleteButton;
            DotNet.invokeMethodAsync('QLSX.Web', 'ButtonClickActionAtBlazorLevel', button.id);
        }
        deleteButton.onclick = function () {
            var button = deleteButton;
            DotNet.invokeMethodAsync('QLSX.Web', 'ButtonClickActionAtBlazorLevel', button.id);
        }
        printButton.onclick = function () {
            var button = deleteButton;
            DotNet.invokeMethodAsync('QLSX.Web', 'ButtonClickActionAtBlazorLevel', button.id);
        }
        content.appendChild(createButton);
        content.appendChild(editButton);
        content.appendChild(deleteButton);
        content.appendChild(printButton);

    }
}

// this code allows calling above functions from a .razor file
igRegisterScript("onUpdatingTemplateColumnNhapXuat", onUpdatingTemplateColumnNhapXuat, false);



igRegisterScript("WebGridCellTemplate", (ctx) => {
    var html = window.igTemplating.html;
    let cellValues = [];
    let uniqueValues = [];
    for (const i of ctx.cell.grid.data) {
        const field = ctx.cell.column.field;
        if (uniqueValues.indexOf(i[field]) === -1) {
            if (ctx.cell.value == i[field]) {
                cellValues.push(html`<igc-select-item selected value=${i[field]}>${(i[field])}</igc-select-item> `);
            } else cellValues.push(html`<igc-select-item value=${i[field]}>${(i[field])}</igc-select-item> `);
            uniqueValues.push(i[field]);
        }
    }
    var deleteButton = document.createElement("button");
    deleteButton.innerHTML = " <igx-icon class='material-icons igx-icon' aria-hidden='true'>delete</igx-icon>"

    var createButton = document.createElement("button");
    createButton.innerHTML = "<igx-icon class='material-icons igx-icon' aria-hidden='true'>add</igx-icon>"

    var editButton = document.createElement("button");
    editButton.innerHTML = " <igx-icon class='material-icons igx-icon' aria-hidden='true'>edit</igx-icon>"

    var printButton = document.createElement("button");
    printButton.innerHTML = " <igx-icon class='material-icons igx-icon' aria-hidden='true'>print</igx-icon>"

   
    createButton.addEventListener("click", function () {
        alert('aaaaaaaaa');
        var button1 = deleteButton;
        DotNet.invokeMethodAsync('QLSX.Web', 'ButtonClickActionAtBlazorLevel', button1.id);
       
    });
    editButton.addEventListener("click", function () {
        var button2 = deleteButton;
        DotNet.invokeMethodAsync('QLSX.Web', 'ButtonClickActionAtBlazorLevel', button2.id);
    });
    deleteButton.addEventListener("click", function () {
        var button3 = deleteButton;
        DotNet.invokeMethodAsync('QLSX.Web', 'ButtonClickActionAtBlazorLevel', button3.id);
    });
    printButton.addEventListener("click", function () {
        var button4 = deleteButton;
        DotNet.invokeMethodAsync('QLSX.Web', 'ButtonClickActionAtBlazorLevel', button4.id);
    });



    var spanHTML = document.createElement("span");
    spanHTML.appendChild(createButton);
    spanHTML.appendChild(editButton);
    spanHTML.appendChild(deleteButton);
    spanHTML.appendChild(printButton);

    return html`
      ${ createButton} ${editButton} ${deleteButton} ${printButton }
`
}, false);

function associateObjRefWithGrid(objRef) {
    var grid = document.querySelector("igc-grid");

    if (grid != null) {
       
        objRef.invokeMethodAsync("ButtonClickActionAtBlazorLevel",1);
    }
}