var user = {
    init: function () {
        this.changeStatus();
    },
    changeStatus: function () {
        $('#btnStatus').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var btn = $(this)
            $.ajax({
                url: '/admin/user/changestatus',
                type: 'post',
                dataType: 'json',
                data: { id },
                success: function (res) {
                    if (res.status) {
                        btn.text("Kích hoạt");
                    } else {
                        btn.text("Khóa");
                    }
                }
            })
        });
    }
}
user.init();