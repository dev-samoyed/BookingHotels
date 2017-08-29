function initDatepicker()
{
    // Datepicker
    $("#startDateInput").datepicker({
        minDate: 0,
        onClose: function (date) {
            var date2 = $('#startDateInput').datepicker('getDate');
            $('#endDateInput').datepicker('setDate', date2);
            $('#endDateInput').datepicker('option', 'minDate', date2);
            $(this).change(); // Forces re-validation
            $('#endDateInput').change();
        }
    }).datepicker("setDate", new Date());
    $('#startDateInput').change();

    $('#endDateInput').datepicker({
        minDate: $('#startDateInput').datepicker('getDate'),
        onClose: function () {
            $("#endDateInput").datepicker("refresh");
            var dt1 = $('#startDateInput').datepicker('getDate');
            var dt2 = $('#endDateInput').datepicker('getDate');
            if (dt2 < dt1) {
                var minDate = $('#endDateInput').datepicker('option', 'minDate');
                $('#endDateInput').datepicker('setDate', minDate);
                $(this).change();
            }
        }
    }).datepicker("setDate", new Date());
    $('#endDateInput').change();
}
