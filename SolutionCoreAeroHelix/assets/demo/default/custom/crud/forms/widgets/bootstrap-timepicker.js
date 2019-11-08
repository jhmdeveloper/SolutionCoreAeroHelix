function currentTime() {
    var today = new Date();
    var hours = today.getHours() % 12 || 12;
    var minutes = today.getMinutes();
    var meridian = today.getHours() >= 12 ? ' PM' : ' AM';
    var time = hours.toString().padStart(2, '0') + ":" + minutes.toString().padStart(2, '0') + meridian;
    
    return time;
}

var BootstrapTimepicker = {
    init: function () {
        $("#m_timepicker_1, #m_timepicker_1_modal").timepicker({
            defaultTime: currentTime()
        }), $("#m_timepicker_2, #m_timepicker_2_modal").timepicker({
            minuteStep: 1,
            defaultTime: "",
            showSeconds: !0,
            showMeridian: !1,
            snapToStep: !0
        }), $("#m_timepicker_3, #m_timepicker_3_modal").timepicker({
            defaultTime: "11:45:20 AM",
            minuteStep: 1,
            showSeconds: !0,
            showMeridian: !0
        }), $("#m_timepicker_4, #m_timepicker_4_modal").timepicker({
            defaultTime: "10:30:20 AM",
            minuteStep: 1,
            showSeconds: !0,
            showMeridian: !0
        }), $("#m_timepicker_1_validate, #m_timepicker_2_validate, #m_timepicker_3_validate").timepicker({
            minuteStep: 1,
            showSeconds: !0,
            showMeridian: !1,
            snapToStep: !0
        })
    }
};
jQuery(document).ready(function () {
    BootstrapTimepicker.init();
});