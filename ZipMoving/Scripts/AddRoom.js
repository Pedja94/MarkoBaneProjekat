function AddRoom()
{
    var room = document.getElementById('RoomToAdd').value;
    $.ajax({
        url: urs.Urls.editUserUrl,
        data: { room: room},
        datatype: 'json',
        success: function (data) {
            var div = $('<div></div>', {
                class: 'col-md-12',
                style: 'margin-top:20px'
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
                text: data.name
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
                    text: "x0"
                });
                span1.append(span2);
                var img = $('<img></img>', {
                    //src: localhost + item.IconLink.replace('~', ''),
                    src: localhost + "/Content/icons/png/005-piano-1.png",
                    style: 'width:24px; height:24px',
                    alt: 'Avatar'
                });
                span2.append(img);
            });


            ////1.
            //var td = $('<td></td>', {
            //});
            //tr.append(td);
            //var span1 = $('<span></span>', {
            //    class: 'badge',
            //    style: 'border-radius:5px;font-size:medium;'
            //});
            //td.append(span1);
            //var span2 = $('<span></span>', {
            //    text: "x10"
            //});
            //span1.append(span2);
            //var img = $('<img></img>', {
            //    src: localhost + "/Content/icons/png/001-stool-2.png",
            //    style: 'width:24px; height:24px',
            //    alt: 'Avatar'
            //});
            //span2.append(img);

            ////2.
            //var td = $('<td></td>', {
            //});
            //tr.append(td);
            //var span1 = $('<span></span>', {
            //    class: 'badge',
            //    style: 'border-radius:5px;font-size:medium;'
            //});
            //td.append(span1);
            //var span2 = $('<span></span>', {
            //    text: "x1"
            //});
            //span1.append(span2);
            //var img = $('<img></img>', {
            //    src: localhost + "/Content/icons/png/002-armchair.png",
            //    style: 'width:24px; height:24px',
            //    alt: 'Avatar'
            //});
            //span2.append(img);

            ////3.
            //var td = $('<td></td>', {
            //});
            //tr.append(td);
            //var span1 = $('<span></span>', {
            //    class: 'badge',
            //    style: 'border-radius:5px;font-size:medium;'
            //});
            //td.append(span1);
            //var span2 = $('<span></span>', {
            //    text: "x23"
            //});
            //span1.append(span2);
            //var img = $('<img></img>', {
            //    src: localhost + "/Content/icons/png/003-stool-1.png",
            //    style: 'width:24px; height:24px',
            //    alt: 'Avatar'
            //});
            //span2.append(img);

            ////4.
            //var td = $('<td></td>', {
            //});
            //tr.append(td);
            //var span1 = $('<span></span>', {
            //    class: 'badge',
            //    style: 'border-radius:5px;font-size:medium;'
            //});
            //td.append(span1);
            //var span2 = $('<span></span>', {
            //    text: "x5"
            //});
            //span1.append(span2);
            //var img = $('<img></img>', {
            //    src: localhost + "/Content/icons/png/004-grand-piano.png",
            //    style: 'width:24px; height:24px',
            //    alt: 'Avatar'
            //});
            //span2.append(img);

            ////5.
            //var td = $('<td></td>', {
            //});
            //tr.append(td);
            //var span1 = $('<span></span>', {
            //    class: 'badge',
            //    style: 'border-radius:5px;font-size:medium;'
            //});
            //td.append(span1);
            //var span2 = $('<span></span>', {
            //    text: "x2"
            //});
            //span1.append(span2);
            //var img = $('<img></img>', {
            //    src: localhost + "/Content/icons/png/005-piano-1.png",
            //    style: 'width:24px; height:24px',
            //    alt: 'Avatar'
            //});
            //span2.append(img);

            //do ovde
       
            var td = $('<td></td>', {
            });
            tr.append(td);
            var span1 = $('<span></span>', {
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


            $("#sobe").append(div);
        },
        error: function () {
            alert("error");
        }
    });
    
}