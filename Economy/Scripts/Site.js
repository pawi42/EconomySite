$(document).ready(function () {
   
   //Initialise any date pickers
    //$('.datepicker').datepicker({
    //    language: 'sv-SE',
    //    format: 'yyyy-MM-dd'
    //});
    
    $("#description").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Ajax/AutoCompleteDescription",
                type: "POST", dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item };
                    }));
                }
            });
        },
        messages: { noResults: "", results: "" }
    });

    $(".datepicker").datepicker({ language: 'sv-SE', autoclose: true, isRTL: false });

    

    $('#amount').click(function () {
        $('#result').text('');
        $(this).select();
    });

    $('#editBill').click(function () {
        $('#result').text('');
    });

    $('#save').click(function () {
        //Some code
        var amount = $('#amount').val();
        var description = $('#description').val();
        var categoryId = $('#category').val();
        var subCategoryId = $('#subCategory').val();
        var payerId = $('#payer').val();
        var dueDate = $('#duedate').val();
        var billId = $('#billId').val();
        var BillJson = JSON.stringify( { BillId: billId, DueDate: dueDate, Amount: amount, Description: description,
            CategoryId: categoryId, SubCategoryId: subCategoryId, PayerId: payerId } );
        $.ajax({
            type: "POST",
            url: "/Ajax/SaveBill",
            data: BillJson,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunc,
            error: errorFunc
        });

        function successFunc(data) {
            $('#result').text('OK');
            $('#result').css('color', 'green');
        }

        function errorFunc() {
            $('#result').text('Felaktiga värden');
            $('#result').css('color', 'red');
        }

    });

    

    $('#monthlyPayments').click(function () {

        $(this).attr("disabled", "disabled");
        var date = JSON.stringify({ date: $('#monthPaymentDate').val() });

        $.ajax({
            type: "POST",
            url: "/Ajax/RunMonthlyPayments",
            data: date,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunc,
            error: errorFunc
        });

        function successFunc(data) {
            $('#result').text('OK');
            $('#result').css('color', 'green');
        }

        function errorFunc() {
            $('#result').text('Felaktiga värden');
            $('#result').css('color', 'red');
        }
    });

  //  $('#monthPaymentDate').datepicker()
  //.on('changeDate', function(ev){
  //    if (ev.date.valueOf() < startDate.valueOf()){
  //        ....
  //    }
  //});

    $("#monthPaymentDate").change(function () {
        //Some code   

        var date = JSON.stringify({ date: $(this).val() });
        
        $.ajax({
            type: "POST",
            url: "/Ajax/IsMonthlyPaymentsExecuted",
            data: date,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunc,
            error: errorFunc
        });

        function successFunc(data, status) {
            //alert(data);
            
                $('#monthlyPayments').attr("disabled", data);
            //else
            //    $('#monthlyPayments').attr("disabled", "");
        }

        function errorFunc() {
            alert('error');
        }

    });

    $("#category").change(function () {
        //Some code   
        
        var categoryId = $(this).val();
        var CategoryJson = JSON.stringify( {CategoryId: categoryId} );
        $.ajax({
            type: "POST",
            url: "/Ajax/GetSubCategories",
            data: CategoryJson,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunc,
            error: errorFunc
        });

        function successFunc(data, status) {
            //alert(data);
            var subCategoryList = $("#subCategory");
            subCategoryList.html("");
            if (Object.keys(data).length > 0) {
                subCategoryList.append($('<option></option>').val(0).html('Välj'));
            }
            else {
                subCategoryList.append($('<option></option>').val(0).html(''));
            }
            var json = $.parseJSON(data);
            $.each(json, function (i, c) {
                subCategoryList.append($('<option></option>').val(c.SubCategoryID).html(c.Name));
            });
            
        }

        function errorFunc() {
            alert('error');
        }

    });

    $('#saveMonthlyBill').click(function () {
        //Some code        
        var amount = $('#amount').val();
        var description = $('#description').val();
        var categoryId = $('#category').val();
        var subCategoryId = $('#subCategory').val();
        var payerId = $('#payer').val();        
        var billId = $('#monthlyBillId').val();
        var BillJson = JSON.stringify({
            BillId: billId, Amount: amount, Description: description,
            CategoryId: categoryId, SubCategoryId: subCategoryId, PayerId: payerId
        });
        $.ajax({
            type: "POST",
            url: "/Ajax/SaveMonthlyBill",
            data: BillJson,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunc,
            error: errorFunc
        });

        function successFunc(data) {
            $('#result').text('OK');
            $('#result').css('color', 'green');
        }

        function errorFunc() {
            $('#result').text('Felaktiga värden');
            $('#result').css('color', 'red');
        }

    });

    $("#filterCategory").change(function () {
        //Some code   

        var categoryId = $(this).val();
        var CategoryJson = JSON.stringify({ CategoryId: categoryId });
        $.ajax({
            type: "POST",
            url: "/Ajax/GetSubCategories",
            data: CategoryJson,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunc,
            error: errorFunc
        });

        function successFunc(data, status) {
            //alert(data);
            var subCategoryList = $("#filterSubCategory");
            subCategoryList.html("");
            if (Object.keys(data).length > 0) {
                subCategoryList.append($('<option></option>').val(0).html('Alla'));
            }
            else {
                subCategoryList.append($('<option></option>').val(0).html(''));
            }
            var json = $.parseJSON(data);
            $.each(json, function (i, c) {
                subCategoryList.append($('<option></option>').val(c.SubCategoryID).html(c.Name));
            });

            getChart();
        }

        function errorFunc() {
            alert('error');
        }

    });

    $("#filterYear").change(function () {
        getChart();
    });

    $("#filterSubCategory").change(function () {
        getChart();
    });

    $("#filterDescription").change(function () {
        getChart();
    });
});

function getChart() {
    var subCategoryId = $(filterSubCategory).val();
    var categoryId = $('#filterCategory').val();
    var description = $('#filterDescription').val();
    var fromYear = $('#filterYear').val();

    var FilterJson = JSON.stringify({ CategoryId: categoryId, SubCategoryId: subCategoryId, Description: description, FromYear: fromYear });
    $.ajax({
        type: "POST",
        url: "/Ajax/GetChart",
        data: FilterJson,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });

    function successFunc(data, status) {
        //alert(data);
        var img = $("#chart");
        img.attr("src", data);
    }

    function errorFunc(xhr, ajaxOptions, thrownError) {
        alert('error');
    }

}