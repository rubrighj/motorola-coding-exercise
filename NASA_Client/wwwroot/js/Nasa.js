class Nasa {
    constructor(globals) {
        $(document).ready(function() {
            RegisterEvents();
        });
        var RegisterEvents = function() {
            $('#search-button').on('click', function(e) {
                e.preventDefault();

                if ($('#search-date').val().length === 0) {
                    Swal.fire('',
                        'You must provide a date to search',
                        'error');
                } else {
                    DownloadRoverImages($('#search-date').val(),
                        $('#select-camera').val(), $('#select-page').val());
                }
            });
        };
        var DownloadRoverImages = function(search_date, camera, page) {
            var request = {};
            request.earth_date = search_date;
            request.camera = camera;
            request.page = page;

            Swal.fire({
                title: '',
                text: 'Retrieving Images...',
                imageUrl: '../img/spinner.gif',
                imageWidth: 200,
                imageHeight: 200,
                allowOutsideClick: false,
                allowEscapeKey: false,
                allowEnterKey: false,
                showCloseButton: false,
                showCancelButton: false,
                showConfirmButton: false
            });
            $.ajax({
                type: "POST",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                headers: {
                    "cache-control": "no-cache",
                    "UserAgent": "Browser"
                },
                url: globals.ServerURL + "api/Image/DownloadRoverImages",
                async: true,
                crossDomain: false,
                data: JSON.stringify(request),
                success: DownloadRoverImagesHandler,
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    Swal.fire('', 'Error Communicating with the Server', 'error');
                }
            });
        };
        var DownloadRoverImagesHandler = function(data) {
            Swal.close();

            var response = data;

            $('#images-collection').html("");

            if (response.responseCode === 0) {
                DisplayProducts(response.localImages);
            }
            else {
                Swal.fire('', response.responseMessage, 'error');
            }
        };
        var DisplayProducts = function (localImages) {
            for (var i = 0; i < localImages.length; i++) {
                for (var j = 0; j < localImages[i].img_paths.length; j++) {
                    var imageSource = '<div class="card ml-2" style="width:325px;">';

                    imageSource += BuildHeader(localImages[i].img_paths[j]);

                    imageSource += BuildImage(localImages[i].img_paths[j]);

                    imageSource += BuildBody();

                    $('#images-collection').append(imageSource);
                }
            }
        };
        var GetFileName = function (path) {
            return path.split('\\').pop().split('/').pop();
        }
        var BuildHeader = function (title) {
            return '<div class="card-header heavy-text">' + GetFileName(title) + '</div>'
        }
        var BuildImage = function (img_path) {
            return '<img class="card-img-top" src="' + img_path.replace("/wwwroot","") +
                '" style="width:200px;height:200px;margin-left:auto;margin-right:auto;margin-top:10px;">';
        }
        var BuildBody = function () {
            return '<div class="card-body"></div></div>';
        }
    }
}