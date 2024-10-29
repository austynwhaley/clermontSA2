$(document).ready(function () {
    // edit button click
    $('.edit-btn').click(function () {
        var id = $(this).data('id');
        var name = $(this).data('name');
        var email = $(this).data('email');
        var phone = $(this).data('phone');
        var address = $(this).data('address');
        var picture = $(this).data('picture');

        $('#userId').val(id);
        $('#userName').val(name);
        $('#userEmail').val(email);
        $('#userPhone').val(phone);
        $('#userAddress').val(address);
        $('#userPicture').val(picture);

        $('#editUserModal').modal('show');
    });

    // close button
    $('.close').click(function () {
        $('#editUserModal').modal('hide');
    });

    // submit
    $('#editUserForm').submit(function (e) {
        e.preventDefault();
        var id = $('#userId').val();
        var name = $('#userName').val();
        var email = $('#userEmail').val();
        var phone = $('#userPhone').val();
        var address = $('#userAddress').val();
        var picture = $('#userPicture').val();

        $.ajax({
            type: 'POST',
            url: '/Main/Update',
            data: {
                Id: id,
                Name: name,
                Email: email,
                Phone: phone,
                Address: address,
                PictureUrl: picture
            },
            success: function (response) {
                if (response.success) {
                    $('#editUserModal').modal('hide');
                    location.reload();
                } else {
                    console.error(response.message);
                    alert(response.message);
                }
            }
        });
    });

    // delete button
    $('.delete-btn').click(function () {
        var id = $(this).data('id');
        $.ajax({
            type: 'POST',
            url: '/Main/Delete',
            data: { id: id },
            success: function (response) {
                if (response.success) {
                    location.reload();
                } else {
                    alert("Error deleting user.");
                }
            }
        });
    });
});
