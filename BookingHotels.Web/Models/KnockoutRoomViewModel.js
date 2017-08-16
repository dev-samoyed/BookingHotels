//var viewModel = function (data) {
//    if (data != null) {
//        ko.mapping.fromJS(data, { Rooms: clientMapping }, self);
//    }
//    var self = this;
//    self.Rooms = ko.observableArray([]);
//}

//var RoomDetail = function (data) { 
//    var self = this; 
//    if (data != null) 
//    { 
//        self.Id = ko.observable(data.ClientName); 
//        self.HotelId = ko.observable(data.Address);
//        self.RoomPrice = ko.observable(data.Phone); 
//        self.RoomType = ko.observable(data.Active);
//        self.Hotel = ko.observable(data.Status); 
//    }
//}

//var clientMapping = {
//    create: function (options) {
//        return new RoomDetail(options.data);
//    }
//};