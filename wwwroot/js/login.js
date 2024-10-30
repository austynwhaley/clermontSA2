document.addEventListener("DOMContentLoaded", function () {

    document.getElementById("adminLink").addEventListener("click", function (event) {
        event.preventDefault();
        console.log("Admin link clicked");
        $('#loginModal').modal('show');
    });

    document.getElementById("loginForm").addEventListener("submit", async function (event) {
        event.preventDefault();

        const username = document.getElementById("username").value;
        const password = document.getElementById("password").value;

        const response = await fetch('/Admin/Login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ username, password }),
        });

        if (response.ok) {
            window.location.href = '/Admin/AdminView';
        } else {
            const errorText = await response.text();
            console.log("Login error response:", errorText);
            document.getElementById("error").innerText = errorText;
        }
    });
});
