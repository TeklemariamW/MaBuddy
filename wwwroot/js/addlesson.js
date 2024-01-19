$(document).ready(function () {

    var lesson = new Vue({

        el: '#lesson',

        data: {
            Lessons: [],
            editLesson: null,

            lesson: {
                LessonTitle: '',
                LessonUrl: '',
                Level: '',
                Title: ''
            }
        },
        created: function () {
            var self = this;

            // Fetch list of Questions
            axios.get("/api/AddLessonApi")
                .then(function (response) {

                    self.Lessons = response.data;
                });
        },
        methods: {
            addLesson: function () {

                var self = this;

                if (self.lesson.LessonTitle.length === 0 || self.lesson.LessonUrl.length === 0)
                    return;

                axios.post('/api/AddLessonApi', self.lesson).then(function (response) {
                    self.Lessons.push(response.data);

                    self.lesson.LessonTitle = '',
                        self.lesson.LessonUrl = '',
                        self.lesson.Level = '',
                        self.lesson.Subject = ''

                })

            },

            remove: function (id) {
                var self = this;

                axios.delete('/api/AddLessonApi/' + id).then(function (response) {

                    self.Lessons = self.Lessons.filter(function (p) {
                        return p.id !== id
                    })

                })
            },
            Edit: function (id) {
                var self = this;


                axios({
                    method: 'put',
                    url: '/api/AddLessonApi/' + id,
                    data: {
                        id: id,
                        LessonTitle: self.lesson.LessonTitle,
                        LessonUrl: self.lesson.LessonUrl,
                        Level: self.lesson.Level,
                        Subject: self.lesson.Subject
                    }
                })
                    .then(function (response) {
                        // handle success
                        self.lesson.LessonTitle = '',
                            self.lesson.LessonUrl = '',
                            self.lesson.Level = '',
                            self.lesson.Subject = ''
                    })
                    .catch(function (error) {
                        // handle error
                        console.log(error);
                    })
                    .then(function () {
                        // Always execute
                        axios.get('/api/AddLessonApi')
                            .then(function (response) {
                                self.Lessons = response.data;
                            })
                    });
            },

        }

    })

});