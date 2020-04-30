$(document).ready(function () {
    $('.btn-photo').on('click', function (e) {
        var id = $(this).data('id');

        console.log('Ok');
        
        $.getJSON('/Home/Photos/' + id, function(data) {
            $('tr#photos' + id).html(tmpl('tmpl-photos', data));
            $('tr#photos' + id).show();
        });
    });
});