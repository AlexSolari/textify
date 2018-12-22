$(function () {
    var TextsEngine = (function () {
        var Engine = {};

        Engine.init = function () {
            $(document).on('click', '.btn-create', function () {
                var $this = $(this);
                var data = $this.parents("form").serialize();
                $this.addClass('disabled');

                $.post("api/textcreate", data, function (html) {
                    $this.removeClass('disabled');
                    $(".cards-content").prepend(html);
                    $($(".cards-content").children()[0]).hide().fadeIn(500);

                    $(".new-text input, .new-text textarea").val("");
                });
            });
            $(document).on('click', '.btn-delete', function () {
                var $this = $(this);
                var deletionConfirmed = confirm("Delete this text entry?");
                $this.addClass("disabled");

                if (deletionConfirmed) {
                    var textId = $this.parents("form").find("input[name='Id']").val();

                    $.post("api/textdelete", { "textId": textId }, function (deleted) {
                        $this.removeClass("disabled");
                        if (deleted) {

                            var $el = $this.parents(".card");
                            $el.fadeOut(500);
                            setTimeout(function () { $el.remove(); }, 500);
                        }
                    });
                }
            });
            $(document).on('click', '.btn-edit', function () {
                var $card = $(this).parents(".card");
                $card.find(".form-group, .btn-save, .btn-cancel").show();
                $card.find(".text-header, .text-value, .btn-edit, .btn-delete").hide();
            });
            $(document).on('click', '.btn-cancel', function () {
                var $card = $(this).parents(".card");
                $card.find(".form-group, .btn-save, .btn-cancel").hide();
                $card.find(".text-header, .text-value, .btn-edit, .btn-delete").show();
            });
            $(document).on('click', '.btn-save', function () {
                var $this = $(this);
                var data = $this.parents("form").serialize();
                $this.addClass("disabled");

                $.post("api/textupdate", data, function (html) {
                    $this.removeClass("disabled");
                    $this.parents('.card').replaceWith(html);
                });
            });

            $(document).on('click', '.btn-show-new-text', function () {
                $("#scriptResult").collapse('hide');
            });
            $(document).on('click', '.btn-generate-script', function () {
                $("#newText").collapse('hide');
            });
        };

        return Engine;
    })();

    TextsEngine.init();
});