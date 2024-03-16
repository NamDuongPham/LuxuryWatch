var sanpham = {
    init: function () {
        sanpham.registerEvents();
    },
    registerEvents: function () {
        $('#btnChooImages').off('click').on('click', function (e){
            e.preventDefault();
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                $('imageList').append('<img src="' + url + '" width="50" />');
            };
            finder.popup();
        })
    }
}
sanpham.init();