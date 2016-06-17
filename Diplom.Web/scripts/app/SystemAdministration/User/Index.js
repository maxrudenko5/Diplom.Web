$(document).ready(function () {

    var localizationParameter = ['DataTableActionsDeactivate', 'DataTableActionsEdit', 'DataTableActionsView'];
    var result = localizedJsFileText(localizationParameter);
    var editBtn = result.DataTableActionsEdit;
    var deactivateBtn = result.DataTableActionsDeactivate;
    var viewBtn = result.DataTableActionsView;
    $('#userDataTable').dataTable({
        language: {
            "url": "/Localization/LocalizedDataTableLanguage",
        },
        dom: '<"html5buttons"B>lTfgitp',
        buttons: [
                { extend: 'excel', title: 'ExcelFile' },
                { extend: 'pdf', title: 'PdfFile' },
        ],
        "ordering": false,
        "info": false,
        "bFilter": false,
        "bLengthChange": false,
        "bProcessing": true,
        "bProcessing": true,
        "sAjaxSource": "/SystemAdministration/User/GetAllUsers",
        "sServerMethod": "GET",
        "sAjaxDataProp": "",
        "aoColumns": [
            { "mData": "UserName" },
            { "mData": "Email" },
            { "mData": "Role.Name" },
            {
                "mData": "CreationDate",

            },
            { "mData": null },
        ],
        "aoColumnDefs": [
            {
                "aTargets": [3],

                "mRender": function (data, type, full) {
                    var dtStart = new Date(parseInt(data.substr(6)));
                    var dtStartWrapper = moment(dtStart);
                    return dtStartWrapper.format('DD/MM/YYYY HH:mm');
                }
            },
            {
                "aTargets": [4],
                "mData": "download_link",
                "mRender": function (data, type, full) {
                    return '<a href="/SystemAdministration/User/Edit?id=' + data.Id + '"> ' + editBtn + ' | <a href="/SystemAdministration/Profile/Edit?id=' + data.Id + '"> ' + viewBtn + ' </a>' + '| <a class="gridLinkDelete" href="#" data-href="/SystemAdministration/User/Delete?id=' + data.Id + '" data-toggle="modal" data-target="#confirm-delete">' + deactivateBtn + '</a>';
                }
            },
        ],
    });
    $('#userDataTable').on('draw.dt', function () {
        $('#confirm-delete').on('show.bs.modal', function (e) {
            $(this).find('.btn-ok').attr('href', $(e.relatedTarget).data('href'));
            $('.debug-url').html('Delete URL: <strong>' + $(this).find('.btn-ok').attr('href') + '</strong>');
        });
    });
});

