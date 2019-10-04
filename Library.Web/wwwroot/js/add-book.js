$('#add-author').click(function () {
    $('#create-author-modal').modal('show');
});

$('#create-author').click(function () {
    const authorNameValue = $('#new-author-name').val();

    $.ajax({
        type: 'POST',
        url: '/bookmanagement/addauthorrest',
        data: { authorName: authorNameValue },
        success: function (response) {
            $('#create-author-modal').modal('hide');

            const option = $(`<option value="${response.id}">${response.name}</option>`);

            $('#authors-selectlist').append(option);

            option.attr('selected', 'selected');
        }
    });
});



$('#add-publisher').click(function () {
   
    $('#create-publisher-modal').modal('show');
});

$('#create-publisher').click(function () {
    const authorPublisherValue = $('#new-publisher-name').val();

    $.ajax({
        type: 'POST',
        url: '/bookmanagement/addpublisherrest',
        data: { publisherName: authorPublisherValue },
        success: function (response) {

            console.log(response);

            $('#create-publisher-modal').modal('hide');

            const option1 = $(`<option value="${response.id}">${response.name}</option>`);

            $('#publisher-selectlist').append(option1);

            option1.attr('selected', 'selected');

            console.log('success');
        }
    });
});

