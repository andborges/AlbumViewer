$(document).ready(function () {
    $('.btn-photo').on('click', function (e) {
        var id = $(this).data('id');

        $('tr.photos').hide();
        
        $.getJSON('/Home/Photos/' + id, function(data) {
            $('tr#photos' + id).html(tmpl('tmpl-photos', data));
            $('tr#photos' + id).show();

            bindComments();
        });
    });

    
});

function bindComments() {
    $('.btn-comments').on('click', function (e) {
        var id = $(this).data('id');

        $('div.comments').hide();
       
        $.getJSON('/Home/Comments/' + id, function(data) {
            $('div#comments' + id).html(tmpl('tmpl-comments', data));
            $('div#comments' + id).show();
        });
    });
}