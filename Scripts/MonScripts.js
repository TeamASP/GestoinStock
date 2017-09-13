/*  ======= sscript rechercher quand le textbox a changer  ==========*/
function recherche() {
        var input, filter, table, tr, td, i;
        input = document.getElementById("myInput");
        filter = input.value.toUpperCase();
        table = document.getElementById("myTable");
        tr = table.getElementsByTagName("tr");
        for (i = 0; i < tr.length; i++) {
            td = tr[i].getElementsByTagName("td")[0];
            if (td) {
                if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                    tr[i].style.display = "";
                } else {
                    tr[i].style.display = "none";
                }
            }
        }
}

$(function () { // will trigger when the document is ready
    $('.datepicker1').datepicker(); //Initialise any date pickers
    $(".datepicker1").datepicker($.datepicker.regional["fr"]);
});
               


function ShowHideDiv(Remember) {
    var dvPassport = document.getElementById("modifier");
    var dvPassport2 = document.getElementById("non_modifier");

    if (Remember.checked) {
        dvPassport.style.display = "block";
        dvPassport2.style.display = "none";
    }
    else {
        dvPassport.style.display = "none";
        dvPassport2.style.display = "block";
    }

}
//========================FiN Sripts rechercher =====================================

//========================== SCRIPT TOOLTIP =====================

$(function () {
    $('[data-toggle="tooltip1"]').tooltip()
})


//==========================  Fin SCRIPT TOOLTIP =====================


$('.deleteItem').click(function (e) {
    e.preventDefault();
    var $ctrl = $(this);
    if (confirm('Do you really want to delete this item?')) {
        $.ajax({
            url: '@Url.Action("Delete")',
            type: 'POST',
            data: { id: $(this).data('id') }
        }).done(function (data) {
            if (data.Result == "OK") {
                $ctrl.closest('tr').remove();
            }
            else if (data.Result.Message) {
                alert(data.Result.Message);
            }
        }).fail(function () {
            alert("There is something wrong. Please try again.");
        })

    }
});

//$(function () {
//    $('.datepicker1').datepicker({
//        format: 'mm/dd/yyyy',
//        startDate: '-3d',
//        todayHighlight: 'true',
//        toggleActive: 'true',
//        autoclose: 'true'
//    });
//});



//$(function () {
//    $('.tooltip-9').tooltip({
//        items: 'a.tooltip-9',
//        content: 'Hello welcome…',
//        show: "slideDown", // show immediately
//        open: function (event, ui) {
//            ui.tooltip.hover(
//            function () {
//                $(this).fadeTo("slow", 0.5);
//            });
//        }
//    });
//});

//$(function () {
//    $('.tooltip-10').tooltip({
//        items: 'a.tooltip-10',
//        content: 'Welcome to TutorialsPoint…',
//        show: "fold",
//        close: function (event, ui) {
//            ui.tooltip.hover(function () {
//                $(this).stop(true).fadeTo(500, 1);
//            },
//            function () {
//                $(this).fadeOut('500', function () {
//                    $(this).remove();
//                });
//            });
//        }
//    });
//});


    //$(document).ready(function () {
    //    var $submenu = $('.submenu');
    //    var $mainmenu = $('.mainmenu');
    //    $submenu.hide();
    //    $submenu.first().delay(400).slideDown(700);
    //    $submenu.on('click', 'li', function () {
    //        $submenu.siblings().find('li').removeClass('chosen');
    //        $(this).addClass('chosen');
    //    });
    //    $mainmenu.on('click', 'li', function () {
    //        $(this).next('.submenu').slideToggle().siblings('.submenu').slideUp();
    //    });
    //    $mainmenu.children('li:last-child').on('click', function () {
    //        $mainmenu.fadeOut().delay(500).fadeIn();
    //    });
    //});

