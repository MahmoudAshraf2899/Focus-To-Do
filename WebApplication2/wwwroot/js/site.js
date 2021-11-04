jQueryAjaxDelete = form => {
    
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    // $('#view-all').html(res.html);
                    $(form).addClass("btn btn-warning").text("Done");
                    $(form).parent().parent().fadeOut(2000);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    

    //prevent default form submit event
    return false;
}