// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}
$(document).ready(function () {

    $('.numberonly').keypress(function (e) {

        var charCode = (e.which) ? e.which : event.keyCode

        if (String.fromCharCode(charCode).match(/[^0-9]/g))

            return false;

    });
    $("#checkAll").click(function () {
        //$(".checkBox").prop('checked',
        //    $(this).prop('checked'));
        if ($(this).is(":checked")) {
            $(".checkBox").prop("checked", true)
        }
        else {
            $(".checkBox").prop("checked", false)
        }
    });

    $("#delete").click(function () {
        //var studentIDs = new Array();
        //$('input:checkbox.checkBox').each(function () {
        //    if ($(this).prop('checked')) {
        //        studentIDs.push($(this).val());

        //    }
            
        /* });*/


        var studentIDs = $('input[name="studentIDs"]:checked').map(function () {
            return this.value;
        }).get().join(",");

        var options = {};
        options.url = "/Student/deletes?studentIDs=" + studentIDs;
        options.type = "POST";
        //options.data = JSON.stringify(customerIDs);
        options.contentType = "application/json";
        options.dataType = "json";
        options.success = function (msg) {
            alert(msg);
        };
        options.error = function () {
            alert("Error while deleting the records!");
        };
        $.ajax(options);

    });
});
jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#view-all').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }
}
