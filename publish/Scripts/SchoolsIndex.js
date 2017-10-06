    $(document).ready(function () {
        $("#SearchState").change(function () {
            var id = $(this).val();
            $.getJSON("/SchoolModels/GetDistricts/", { sStateCode: id },
            function (data) {
                var select = $("#SearchDistrict");
                select.empty();
                select.append($('<option/>', {
                    value: 0,
                    text: "- Select a District -"
                }));
                $.each(data, function (index, data) {

                    select.append($('<option/>', {
                        value: data.Value,
                        text: data.Text
                    }));
                });
            });
        });
    });
