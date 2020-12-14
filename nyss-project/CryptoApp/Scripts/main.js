$(function () {

    function printError(errorMessage) {
        $('.textarea_output').val(errorMessage);
    }

    function validateFile() {
        let fileExtensions = ['txt', 'docx'];
        let fileInput = $('.input_file');

        return fileInput.get(0).files.length !== 0 &&
               $.inArray(fileInput.val().split('.').pop().toLowerCase(), fileExtensions) !== -1;
    }

    function validateInputTextArea() {
        let textAreaInput = $('.textarea_input');

        return $.trim(textAreaInput.val())
    }

    function validateInputKey() {
        let keyInput = $('.controls__key-input');

        return $.trim(keyInput.val()) && !/[^а-яё]/i.test(keyInput.val());
    }

    function preActionValidate() {
        if (!validateInputTextArea()) {
            printError('Введите корректный текст для обработки!');
            return false;
        }
        if (!validateInputKey()) {
            printError('Введите корректный ключ шифрования! Ключ может содержать только символы русского алфавита.');
            return false;
        }

        return true;
    }
    
    $('.input__button_upload').on('click', function (event) {
        event.preventDefault();

        if (!validateFile()) {
            printError('Выберите файл с расширением .txt или .docx!');
        } else {
            $.ajax({
                url: '/home/upload',
                type: 'POST',
                data: new FormData($('.input-form')[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    $('.textarea_input').val(data);
                },
                error: function (xhr, error, status) {
                    printError('При загрузке файла на сервер произошла ошибка!')
                }
            });
        }
    });

    $('.controls__button_encrypt').on('click', function (event) {
        event.preventDefault();

        if (preActionValidate()) {
            $.ajax({
                url: '/home/encrypt',
                type: 'POST',
                data: new FormData($('.input-form')[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    $('.textarea_output').val(data);
                },
                error: function (xhr, error, status) {
                    printError('При отправке запроса на сервер произошла ошибка!')
                }
            });
        }
    });

    $('.controls__button_decrypt').on('click', function (event) {
        event.preventDefault();

        if (preActionValidate()) {
            $.ajax({
                url: '/home/decrypt',
                type: 'POST',
                data: new FormData($('.input-form')[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    $('.textarea_output').val(data);
                },
                error: function (xhr, error, status) {
                    printError('При отправке запроса на сервер произошла ошибка!')
                }
            });
        }
    });

});
