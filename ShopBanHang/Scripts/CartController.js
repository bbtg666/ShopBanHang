(function () {
    $(".input-mini").blur(function () {
        var id = $(this).data('id');
        var quantity = $(this).val();
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '/cart/update',
            data: { id, quantity },
            success: function (res) {
                if (res.status == true) {
                    window.location.href = "/gio-hang";
                }
            }
        })
    })
    $('#btnDelete').click(function () {
        var listCartItem = [];
        $('.checkbox:checked').each(function (i, item) {
            listCartItem.push($(item).data('id'));
        })
        $.ajax({
            type: "POST",
            dataType: "json",
            data: { listID: JSON.stringify(listCartItem) },
            url: "/cart/delete",
            success: function (res) {
                if (res.status == true) {
                    window.location.href = "/gio-hang";
                }
            }
        })
    });
    $('#btnContinue').click(function () {
        window.location.href = "/";
    });
})();