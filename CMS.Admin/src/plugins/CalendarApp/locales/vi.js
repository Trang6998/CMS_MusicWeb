
export default {
    promptLabels: {
        actionRemove: 'Bạn có chắc muốn xóa sự kiện này?',
        actionExclude: 'Bạn có chắc chắn muốn xóa sự kiện này không?',
        actionCancel: 'Bạn có chắc muốn hủy sự kiện này?',
        actionUncancel: 'Bạn có chắc chắn muốn hủy sự kiện này không?',
        actionSetStart: 'Bạn có chắc chắn muốn đặt sự kiện này là lần đầu tiên không?',
        actionSetEnd: 'Bạn có chắc chắn muốn đặt lần xuất hiện này là lần cuối cùng không?',
        actionMove: 'Bạn có chắc chắn muốn di chuyển sự kiện này không?',
        actionInclude: 'Bạn có chắc chắn muốn thêm sự kiện xảy ra không?',
        move: 'Bạn có chắc chắn muốn di chuyển sự kiện này không?',
        toggleAllDay: 'Bạn có chắc chắn muốn thay đổi liệu điều này chắc chắn bạn muốn thay đổi xem sự kiện này có xảy ra cả ngày không? Sự kiện xảy ra cả ngày?',
        removeExistingTime: 'Bạn có chắc chắn muốn xóa tất cả các sự kiện xảy ra tại thời điểm này không?'
    },
    placeholder: {
        noTitle: '(Không có nội dung)'
    },
    patterns: {
        lastDay: (day) => 'Ngày cuối cùng của tháng',
        lastDayOfMonth: (day) => 'Ngày cuối cùng của ' + day.format('MMMM'),
        lastWeekday: (day) => 'Cuối ' + day.format('dddd') + ' trong ' + day.format('MMMM')
    },
    colors: [
        { text: 'Red' },
        { text: 'Pink' },
        { text: 'Purple' },
        { text: 'Deep Purple' },
        { text: 'Indigo' },
        { text: 'Blue' },
        { text: 'Glue' },
        { text: 'Light Blue' },
        { text: 'Cyan' },
        { text: 'Teal' },
        { text: 'Green' },
        { text: 'Light Green' },
        { text: 'Lime' },
        { text: 'Yellow' },
        { text: 'Amber' },
        { text: 'Orange' },
        { text: 'Deep Orange' },
        { text: 'Brown' },
        { text: 'Blue Gray' },
        { text: 'Gray' },
        { text: 'Black' }
    ],
    icons: [
        { text: 'Họp'},
        { text: 'Quan trọng' },
        { text: 'Love' },
        { text: 'Action' },
        { text: 'Assignment' },
        { text: 'Warning' },
        { text: 'Money' },
        { text: 'Charge' },
        { text: 'Home' },
        { text: 'Play' },
        { text: 'Email' },
        { text: 'Phone' },
        { text: 'Chart' },
        { text: 'Biking' },
        { text: 'Travel' }
    ],
    defaults: {
        dsDay: {
            formats: {
                month: 'MMM'
            }
        },
        dsCalendarApp: {
            types: [
                { label: 'Ngày' },
                { label: 'Tuần' },
                { label: 'Tháng' },
                { label: 'Năm' },
                {  label: 'Danh sách' },
                { label: '4 ngày' }
            ],
            formats: {
                today: 'dddd, MMMM D',
                xs: 'MMM'
            },
            labels: {
                next: (type) => type ? 'Sau ' + type.label.toLowerCase() : 'Sau',
                prev: (type) => type ? 'Trước ' + type.label.toLowerCase() : 'Trước',
                moveCancel: 'Hủy di chuyển',
                moveSingleEvent: 'Di chuyển sự kiện',
                moveOccurrence: 'Chỉ di chuyển sự kiện này',
                moveAll: 'Di chuyển tất cả các sự kiện xảy ra',
                moveDuplicate: 'Thêm sự kiện xảy ra',
                promptConfirm: 'Có',
                promptCancel: 'Không',
                today: 'Hôm nay'
            }
        },
        dsAgendaEvent: {
            formats: {
                firstLine: 'ddd',
                secondLine: 'MMM Do',
                start: 'dddd, MMMM D',
                time: 'h:mm a'
            },
            labels: {
                allDay: 'Cả ngày',
                options: 'Tùy chọn',
                close: 'Đóng',
                day: ['day', 'ngày'],
                days: ['day', 'ngày'],
                minute: ['minute', 'phút'],
                minutes: ['minute', 'phút'],
                hour: ['hour', 'giờ'],
                hours: ['hour', 'giờ'],
                week: ['week', 'tuần'],
                weeks: ['week', 'tuần'],
                second: ['second', 'giây'],
                seconds: ['second', 'giây'],
                busy: 'Hiển thị',
                free: 'Ẩn'
            }
        },
        dsCalendarEventChip: {
            formats: {
                fullDay: 'ddd MMM Do YYYY',
                timed: 'ddd MMM Do YYYY'
            }
        },
        dsCalendarEventPopover: {
            formats: {
                start: 'dddd, MMMM D',
                time: 'h:mm a'
            },
            labels: {
                allDay: 'Cả ngày',
                options: 'Tùy chọn',
                close: 'Đóng',
                day: ['day', 'ngày'],
                days: ['day', 'ngày'],
                minute: ['minute', 'phút'],
                minutes: ['minute', 'phút'],
                hour: ['hour', 'giờ'],
                hours: ['hour', 'giờ'],
                week: ['week', 'tuần'],
                weeks: ['week', 'tuần'],
                second: ['second', 'giây'],
                seconds: ['second', 'giây'],
                busy: 'Hiển thị',
                free: 'Ẩn'
            }
        },
        dsCalendarEventCreatePopover: {
            formats: {
                start: 'dddd, MMMM D',
                time: 'h:mm a'
            },
            labels: {
                title: 'Thêm nội dung',
                allDay: 'Cả ngày',
                close: 'Đóng',
                save: 'Lưu',
                day: ['day', 'ngày'],
                days: ['day', 'ngày'],
                minute: ['minute', 'phút'],
                minutes: ['minute', 'phút'],
                hour: ['hour', 'giờ'],
                hours: ['hour', 'giờ'],
                week: ['week', 'tuần'],
                weeks: ['week', 'tuần'],
                second: ['second', 'giây'],
                seconds: ['second', 'giây'],
                busy: 'Hiển thị',
                free: 'Ẩn',
                location: 'Thêm địa điểm',
                description: 'Thêm thành phần',
                calendar: 'Chủ trì',
            },
            busyOptions: [
                { text: 'Hiển thị' },
                { text: 'Ẩn' }
            ]
        },
        dsSchedule: {
            labels: {
                editCustom: 'Sửa'
            }
        },
        dsEvent: {
            labels: {
                cancel: 'Hủy thay đổi sự kiện',
                save: 'Lưu',
                title: 'Nội dung',
                exclusions: 'Đây là những sự kiện hoặc khoảng thời gian mà một sự kiện thường xảy ra đã bị loại trừ khỏi lịch biểu. Sự kiện bị loại trừ ở đây nếu sự kiện xảy ra được di chuyển.',
                inclusions: 'Đây là những sự kiện hoặc thời gian mà các sự kiện được thêm vào bên ngoài lịch biểu thường xảy ra. Sự kiện được thêm vào đây nếu sự kiện xảy ra được di chuyển.',
                cancelled: 'Đây là những sự kiện hoặc khoảng thời gian mà các sự kiện đã bị hủy.',
                edit: 'Sửa sự kiện',
                add: 'Thêm sự kiện',
                location: 'Thêm địa điểm',
                description: 'Thêm thành phần',
                calendar: 'Chủ trì',
                tabs: {
                    details: 'Chi tiết sự kiện',
                    forecast: 'Dự báo',
                    removed: 'Đã xóa',
                    added: 'Đã thêm',
                    cancelled: 'Đã hủy'
                }
            },
            busyOptions: [
                { text: 'Hiển thị' },
                { text: 'Ẩn' }
            ]
        },
        dsScheduleActions: {
            labels: {
                remove: 'Xóa sự kiện này',
                exclude: 'Xóa sự kiện này',
                cancel: 'Hủy sự kiện này',
                uncancel: 'Hoàn tác hủy',
                move: 'Di chuyển sự kiện này',
                include: 'Thêm lần xuất hiện mới',
                setStart: 'Đặt làm lần xuất hiện đầu tiên',
                setEnd: 'Đặt làm lần xuất hiện cuối cùng',
                pickerOk: 'OK',
                pickerCancel: 'Hủy'
            }
        },
        dsScheduleForecast: {
            labels: {
                prefix: 'Dự báo hiển thị trước đó và tiếp theo',
                suffix: 'sự kiện xảy ra trong vòng một năm.'
            }
        },
        dsScheduleFrequencyDay: {
            labels: {
                type: 'Ngày'
            },
            options: [
                { text: 'Ngày bất kỳ' },
                { text: 'Vào những ngày tiếp theo ...' },
                { text: 'Mỗi _ ngày bắt đầu vào _' }
            ],
            types: [
                { text: 'Ngày trong tháng' },
                { text: 'Ngày cuối cùng của tháng' },
                { text: 'Ngày trong năm' }
            ]
        },
        dsScheduleFrequencyDayOfWeek: {
            weekdays: ['Chủ nhật', 'Thứ 2', 'Thứ 3', 'Thứ 4', 'Thứ 5', 'Thứ 6', 'Thứ 7'],
            labels: {
                type: 'Ngày trong tuần'
            },
            options: [
                { text: 'Ngày bất kỳ trong tuần' },
                { text: 'On the following days of the week...' },
                { text: 'Mỗi _ ngày trong tuần bắt đầu vào _' },
                { text: 'Các ngày cuối tuần' },
                { text: 'Các ngày trong tuần' }
            ]
        },
        dsScheduleFrequencyMonth: {
            labels: {
                type: 'Tháng'
            },
            months: [
                'Tháng ',
                'Tháng 2',
                'Tháng 3',
                'Tháng 4',
                'Tháng 5',
                'Tháng 6',
                'Tháng 7',
                'Tháng 8',
                'Tháng 9',
                'Tháng 10',
                'Tháng 11',
                'Tháng 12'
            ],
            options: [
                { text: 'Bất kỳ tháng nào' },
                { text: 'Vào những tháng tiếp theo...' },
                { text: 'Mỗi _ tháng bắt đầu từ _' }
            ]
        },

        dsScheduleFrequencyWeek: {
            labels: {
                type: 'Tuần'
            },
            options: [
                { text: 'Bất kỳ tuần nào' },
                { text: 'Vào các tuần tiếp theo...' },
                { text: 'Mỗi _ tuần bắt đầu vào _' }
            ],
            types: [
                { text: 'Tuần của tháng (tuần đầu tiên có Thứ Năm)' },
                { text: 'Weekspan của tháng (bắt đầu vào ngày đầu tiên của tháng)' },
                { text: 'Tuần đầy đủ của tháng (0th = tuần trước nếu có)' },
                { text: 'Tuần cuối tuần của tháng (bắt đầu vào ngày cuối cùng của tháng)' },
                { text: 'Tuần đầy đủ cuối cùng của tháng (0th = tuần sau nếu có)' },
                { text: 'Tuần trong năm (tuần đầu tiên có Thứ Năm)' },
                { text: 'Weekspan của năm (bắt đầu vào ngày đầu tiên của năm)' },
                { text: 'Cả tuần trong năm (0th = tuần trước nếu có)' },
                { text: 'Tuần trước của năm (bắt đầu vào ngày cuối cùng của năm)' },
                { text: 'Tuần cuối cùng của năm (0th = tuần sau nếu có)' }
            ]
        },

        dsScheduleFrequencyYear: {
            labels: {
                type: 'Năm'
            },
            options: [
                {
                    text: 'Bất kỳ năm nào' },
                { text: 'Những năm tiếp theo...' },
                { text: 'Mỗi _ năm bắt đầu từ _' }
            ]
        },

        dsScheduleSpan: {
            labels: {
                startless: 'Thời gian bắt đầu',
                endless: 'Thời gian kết thúc'
            },
            formats: {
                start: 'MMMM Do, YYYY',
                end: 'MMMM Do, YYYY'
            }
        },

        dsScheduleTime: {
            labels: {
                remove: 'Xóa thời gian',
                add: 'Thêm thời gian'
            }
        },

        dsScheduleTimes: {
            labels: {
                all: 'Cả ngày',
                minute: 'phút',
                minutes: 'phút',
                hour: 'giờ',
                hours: 'giờ',
                day: 'ngày',
                days: 'ngày',
                week: 'tuần',
                weeks: 'tuần',
                month: 'tháng',
                months: 'tháng',
                second: 'giây',
                seconds: 'giây'
            }
        },

        dsScheduleType: {
            formats: {
                date: 'LL'
            }
        },

        dsScheduleTypeCustomDialog: {
            labels: {
                save: 'Lưu',
                cancel: 'Hủy'
            }
        },

        dsWeekDayHeader: {
            formats: {
                weekday: 'ddd'
            }
        },

        dsWeeksView: {
            weekdays: ['CN', 'Th2', 'Th3', 'Th4', 'Th5', 'Th6', 'Th7']
        },

        dsDaysView: {
            hours: [
                '    ', '1am', '2am', '3am', '4am', '5am', '6am', '7am', '8am', '9am', '10am', '11am',
                '12pm', '1pm', '2pm', '3pm', '4pm', '5pm', '6pm', '7pm', '8pm', '9pm', '10pm', '11pm'
            ]
        },

        dsDayPicker: {
            weekdays: ['Chủ nhật', 'Thứ 2', 'Thứ 3', 'Thứ 4', 'Thứ 5', 'Thư 6', 'Thứ 7'],
            labels: {
                prevMonth: 'Tháng trước',
                nextMonth: 'Tháng sau'
            }
        }
    }
}