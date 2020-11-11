var socket = null;
function printBill(bill) {
    if (Settings.remote_printing == 1) {
        Popup($('#bill_tbl').html());
    } else if (Settings.remote_printing == 2) {
        if (socket.readyState == 1) {
            if (Settings.print_img == 1) {
                $('#bill-data').show();
                $('#preb').html(
                    '<pre style="background:#FFF;font-size:20px;margin:0;border:0;color:#000 !important;">' +
                    bill_data.info +
                    bill_data.items +
                    '\n' +
                    bill_data.totals +
                    '</pre>'
                );
                var element = $('#bill-data').get(0);
                html2canvas(element, { scrollY: 0, scale: 1.7 }).then(function (canvas) {
                    var dataURL = canvas.toDataURL();
                    var socket_data = {
                        'printer': false,
                        'text': dataURL, 'cash_drawer': 0
                    };
                    socket.send(JSON.stringify({
                        type: 'print-img',
                        data: socket_data
                    }));
                    // return Canvas2Image.saveAsPNG(canvas);
                });
                setTimeout(function () {
                    $('#bill-data').hide();
                }, 500);
            } else {
                var socket_data = { 'printer': false, 'logo': '~/uploads/logo.png', 'text': bill };
                socket.send(JSON.stringify({
                    type: 'print-receipt',
                    data: socket_data
                }));
            }
            return false;
        } else {
            bootbox.alert('Unable to connect to socket, please make sure that server is up and running fine.');
            return false;
        }
    }
}
var order_printers = [];
function printOrder(order) {
    if (Settings.remote_printing == 1) {
        Popup($('#order_tbl').html());
    } else if (Settings.remote_printing == 2) {
        if (socket.readyState == 1) {
            if (Settings.print_img == 1) {
                $('#order-data').show();
                $('#preo').html(
                    '<pre style="background:#FFF;font-size:20px;margin:0;border:0;color:#000 !important;">' + order_data.info + order_data.items + '</pre>'
                );
                var element = $('#order-data').get(0);
                html2canvas(element, { scrollY: 0, scale: 1.7 }).then(function (canvas) {
                    var dataURL = canvas.toDataURL();
                    var socket_data = {
                        'printer': false,
                        'text': dataURL, 'order': 1, 'cash_drawer': 0
                    };
                    socket.send(JSON.stringify({
                        type: 'print-img',
                        data: socket_data
                    }));
                    // return Canvas2Image.saveAsPNG(canvas);
                });
                setTimeout(function () {
                    $('#order-data').hide();
                }, 500);
            } else {
                if (order_printers == '') {
                    var socket_data = {
                        'printer': false, 'order': true,
                        'logo': '~/uploads/logo.png',
                        'text': order
                    };
                    socket.send(JSON.stringify({ type: 'print-receipt', data: socket_data }));
                } else {
                    $.each(order_printers, function () {
                        var socket_data = { 'printer': this, 'logo': '~/uploads/logo.png', 'text': order };
                        socket.send(JSON.stringify({ type: 'print-receipt', data: socket_data }));
                    });
                }
            }
            return false;
        } else {
            bootbox.alert('Unable to connect to socket, please make sure that server is up and running fine.');
            return false;
        }
    }
}