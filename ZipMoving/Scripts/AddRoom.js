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

            span1.click(function () {
                $("#targetItems").innerHTML = '';
                $.ajax({
                    url: urlItems.Urls.editUserUrl,
                    data: { room: room },
                    datatype: 'json',
                    success: function (data) {

                        
                        
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
                                text: 'x0'
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

                            var div4 = $('<div></div>', {
                                class: 'col-xs-6'
                            });
                            div2.append(div4);

                            var span2 = $('<span></span>', {
                                class: 'glyphiconMinus glyphicon glyphicon-minus'
                            });
                            div4.append(span2);

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

function plus()
{
    var ikonica = document.getElementById('ikonica');
    var vrednost = ikonica.innerText;
    vrednost = vrednost.replace('x', '');
    vrednost++;
    ikonica.innerText = " x" + vrednost;
}