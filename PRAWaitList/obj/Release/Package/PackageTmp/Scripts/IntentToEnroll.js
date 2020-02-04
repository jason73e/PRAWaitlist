
    $(document).ready(function () {
        $("#Student_sm_LocalDistrict").change(function () {
            var id = $(this).val();
            $.getJSON("/IntentToEnroll/GetSchools/", { sDistrictCode: id },
            function (data) {
                var select = $("#Student_sm_LocalSchool");
                select.empty();
                select.append($('<option/>', {
                    value: 0,
                    text: "- Select a School -"
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

    $(document).ready(function () {
        $("#Family_fm_StateID").change(function () {
            var id = $(this).val();
            $.getJSON("/IntentToEnroll/GetDistricts/", { sStateCode: id },
            function (data) {
                var select = $("#Student_sm_LocalDistrict");
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

    $(document).ready(function () {
        $("#btnCreate").click(function () {
            $("#frmCreate").removeData("validator");
            $("#frmCreate").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("#frmCreate");
        });
    });

