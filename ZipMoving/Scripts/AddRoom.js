var NumberOfItemsMatrix = [];
for (var i = 0; i < 9; i++)
    NumberOfItemsMatrix[i] = [];

function AddRoom()
{  
    var room = document.getElementById('RoomToAdd').value;

    $.ajax({
        url: url.Urls.editUserUrl,
        data: { room : room},
        datatype: 'json',
        success: function (data) {
            
            for (var i = 0; i < data.items.length; i++)
                NumberOfItemsMatrix[parseInt(room) - 1].push(0); //postavljamo sve iteme u sobi na x0

            //brisanje sobe iz dropdown-a
            document.getElementById("RoomToAdd" + data.SelectedRoom.Id).remove();

            //sacuvan i id sobe koja je dodata zbog brisanja
            var div = $('<div></div>', {
                class: 'col-md-12',
                style: 'margin-top:20px',
                id: "AddedRoom" + data.SelectedRoom.Id
            });

            var table = $('<table></table>', {
                class: 'table table-bordered',
                style: 'background-color:white; border-right:none; border-bottom:none;'
            });
            div.append(table);
            var br = $("<br />");
            div.append(br);
            
            var thead = $('<thead></thead>', {
            });
            table.append(thead);

            var tr = $('<tr></tr>', {
            });
            thead.append(tr);

            var th = $('<th></th>', {
                class: 'col-md-2',
                style: 'background-color:#F05F40;color:white; text-align:center; font-weight:700;border-bottom:none;',
                text: data.SelectedRoom.Name
            });
            tr.append(th);

            var tbody = $('<tbody></tbody>', {
            });
            table.append(tbody);

            var tr = $('<tr></tr>', {
                style: 'text-align:center;'
            });
            tbody.append(tr);

            var td = $('<td></td>', {
                style: 'text-align:center;font-weight:700;',
                text: "Room Items:"
            });
            tr.append(td);

            $.each(data.items, function (index, item) {
                //var obj = jQuery.parseJSON(item);
                var obj = item;

                var td = $('<td></td>', {
                });
                tr.append(td);
                var span1 = $('<span></span>', {
                    class: 'badge',
                    style: 'border-radius:5px;font-size:medium;'
                });
                td.append(span1);
                var span2 = $('<span></span>', {
                    text: "x" + NumberOfItemsMatrix[parseInt(room) - 1][index],
                    id: "SpanEdit" + item.Id
                });
                span1.append(span2);
                var img = $('<img></img>', {
                    src: localhost + item.IconLink.replace('~', ''),
                    style: 'width:24px; height:24px',
                    alt: 'Avatar'
                });
                span2.append(img);

            });
       
            var td = $('<td></td>', {
            });
            tr.append(td);
            var span1 = $('<span></span>', {
                id: "room" + room
            });
            span1.attr("data-toggle", "modal");
            span1.attr("data-target", "#editModal");
            td.append(span1);
            var span2 = $('<span></span>', {
                class: 'glyphicon glyphicon-edit',
                style: 'cursor:pointer;color:#F05F40;'
            });
            span2.attr("data-toggle", "tooltip");
            span2.attr("title", "Edit Existing Room");
            span1.append(span2);


            //START brisanje sobe

            var td = $('<td></td>', {
            });
            tr.append(td);
            var span3 = $('<span></span>', {
                class: 'glyphicon glyphicon-trash',
                style: 'cursor:pointer;color:#F05F40;',
                id: "Delete" + data.SelectedRoom.Id
            });
            span3.attr("data-toggle", "tooltip");
            span3.attr("title", "Delete Room");
            td.append(span3);

            span3.click(function () {
                var id = this.id.replace("Delete", "");
                $.ajax({
                    url: remove.Urls.editUserUrl,
                    data: { soba: parseInt(id) },
                    datatype: 'json',
                    success: function (data) {
                        document.getElementById("AddedRoom" + id).remove();

                        while (NumberOfItemsMatrix[parseInt(id) - 1].length > 0) {
                            NumberOfItemsMatrix[parseInt(id) - 1].pop();
                        }

                        //vracanje sobe u dropdown
                        var option = $('<option></option>', {
                            value: id,
                            id: "RoomToAdd" + id,
                            text: data.SelectedRoom.Name
                        });

                        $("#RoomToAdd").append(option);
                    },
                    error: function () {
                        alert("error");
                    }
                });
            });

            //END brisanje sobe








            span1.click(function () {              
                $.ajax({
                    url: url.Urls.editUserUrl,
                    data: { room : this.id.replace("room", "") },
                    datatype: 'json',
                    success: function (data) {

                        $("#editModal").empty();

                        var ModalDialogDiv = $('<div></div>', {
                            class: 'modal-dialog',
                            style: 'width:70%;'
                        });
                        $("#editModal").append(ModalDialogDiv);

                        var ModalContentDiv = $('<div></div>', {
                            class: 'modal-content',
                        });
                        ModalDialogDiv.append(ModalContentDiv);

                        var ModalHeaderDiv = $('<div></div>', {
                            class: 'modal-header',
                            style: 'background-color:white; color:#F05F40;'
                        });
                        ModalContentDiv.append(ModalHeaderDiv);

                        var ModalTitle = $('<h4></h4>', {
                            class: 'modal-title',
                            style: 'font-weight:700;',
                            text: data.SelectedRoom.Name + " Edit"
                        });
                        ModalHeaderDiv.append(ModalTitle);

                        var ModalBodyDiv = $('<div></div>', {
                            class: 'modal-body text-center',
                            style: 'border-bottom:none;'
                        });
                        ModalContentDiv.append(ModalBodyDiv);

                        var ItemsDiv = $('<div></div>', {
                            class: 'row',
                            style: 'margin-left:10px; margin-right:10px;',
                            id: "targetItems"
                        });
                        ModalBodyDiv.append(ItemsDiv);

                        var ModalFooterDiv = $('<div></div>', {
                            class: 'modal-footer',
                            style: 'text-align:center; border-top:none;'
                        });
                        ModalContentDiv.append(ModalFooterDiv);

                        var paragraf = $('<p></p>', {
                            style: 'font-size:small;color:#F05f40;',
                            text: "Editing Your Room: After you have finished adding and substracting your items, click on \"Save & Close\" to go back to Your Online Inventory. "
                        });
                        ModalFooterDiv.append(paragraf);

                        var SaveClose = $('<button></button>', {
                            type: 'button',
                            class: 'btn btn-default',
                            style: 'color:#F05f40;',
                            text: "Save & Close",
                            id: "save" + data.SelectedRoom.Id
                        });
                        SaveClose.attr("data-dismiss", "modal");
                        ModalFooterDiv.append(SaveClose);

                        var Ok = $('<span></span>', {
                            class: 'glyphicon glyphicon-ok'
                        });
                        SaveClose.append(Ok);

                        SaveClose.click(function () {
                            $.ajax({
                                url: url.Urls.editUserUrl,
                                data: { room : this.id.replace("save", "") },
                                datatype: 'json',
                                success: function (data) {

                                    $.each(data.items, function (index, item) {
                                        NumberOfItemsMatrix[parseInt(data.SelectedRoom.Id) - 1][index] = parseInt(($("#item" + item.Id).text()).replace("x", ""));
                                        //promena u osnovnom editu nije moguca jer smo trenutno u modalu
                                        $("#SpanEdit" + item.Id).html($("#SpanEdit" + item.Id).html().replace($("#SpanEdit" + item.Id).text(), $("#item" + item.Id).text()));
                                    });


                                    $.ajax({
                                        url: collect.Urls.editUserUrl,
                                        data: { soba: parseInt(data.SelectedRoom.Id), niz: NumberOfItemsMatrix[parseInt(data.SelectedRoom.Id) - 1] },
                                        datatype: 'json',
                                        traditional: true,
                                        success: function (data) {
                                            var kec = data.success;
                                        },
                                        error: function () {
                                            alert("error");
                                        }
                                    });


                                },
                                error: function () {
                                    alert("error");
                                }
                            });
                        });


                        
                        //$("#targetItems").empty();

                        $.each(data.items, function (index, item) {
                            var div = $('<div></div>', {
                                class: 'col-md-2',
                                style: 'margin-top:10px; margin-bottom:10px;'
                            });

                            var div1 = $('<div></div>', {
                                class: 'col-md-12',
                                style: 'margin-top:10px; margin-bottom:10px;'
                            });
                            div.append(div1);

                            var div2 = $('<div></div>', {
                                class: 'col-md-12 badge',
                                style: 'display:inline-block; margin-top:4px; margin-bottom:4px;background-color:#e8e8e8;'
                            });
                            div.append(div2);

                            var span = $('<span></span>', {
                                class: '',
                                style: 'font-size:x-large;',
                                text: "x" + NumberOfItemsMatrix[data.SelectedRoom.Id - 1][index],
                                id: "item" + item.Id
                            });
                            div1.append(span);

                            var img = $('<img></img>', {
                                src: localhost + item.IconLink.replace('~', ''),
                                style: 'width:25px; height:25px',
                                alt: 'Avatar'
                            });
                            span.append(img);

                            var div3 = $('<div></div>', {
                                class: 'col-xs-6'
                            });
                            div2.append(div3);

                            var span1 = $('<span></span>', {
                                class: 'glyphiconPlus glyphicon glyphicon-plus'
                            });
                            div3.append(span1);

                            //start
                            span1.click(function () {
                                $.ajax({
                                    data: { },
                                    success: function (data) {

                                        var ikonica = document.getElementById("item" + item.Id);
                                        var vrednost = ikonica.innerText;
                                        vrednost = vrednost.replace('x', '');
                                        vrednost++;
                                        //ikonica.innerText = " x" + vrednost;

                                        //malo bezveze napravljeno, menjano zbog ove slike bitno da radi
                                        $("#item" + item.Id).html($("#item" + item.Id).html().replace(ikonica.innerText, " x" + vrednost));
                                        //var img2 = $('<img></img>', {
                                        //    src: localhost + item.IconLink.replace('~', ''),
                                        //    style: 'width:25px; height:25px',
                                        //    alt: 'Avatar'
                                        //});
                                        //ikonica.append(img2);
                                        
                                    },
                                    error: function () {
                                        alert("error");
                                    }
                                });
                            });
                            //end

                            var div4 = $('<div></div>', {
                                class: 'col-xs-6'
                            });
                            div2.append(div4);

                            var span2 = $('<span></span>', {
                                class: 'glyphiconMinus glyphicon glyphicon-minus'
                            });
                            div4.append(span2);

                            //start
                            span2.click(function () {
                                $.ajax({
                                    data: { },
                                    success: function (data) {

                                        var ikonica = document.getElementById("item" + item.Id);
                                        var vrednost = ikonica.innerText;
                                        vrednost = vrednost.replace('x', '');
                                        vrednost--;
                                        if (vrednost < 0)
                                            vrednost = 0;
                                        //ikonica.innerText = " x" + vrednost;

                                        $("#item" + item.Id).html($("#item" + item.Id).html().replace(ikonica.innerText, " x" + vrednost));

                                    },
                                    error: function () {
                                        alert("error");
                                    }
                                });
                            });
                            //end

                            $("#targetItems").append(div);
                        });         
                    },
                    error: function () {
                        alert("error");
                    }
                });
            });
            


            $("#sobe").append(div);
        },
        error: function () {
            alert("error");
        }
    });
    
}