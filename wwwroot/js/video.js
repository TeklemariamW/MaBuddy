$(document).ready(function () {

    var video = new Vue({

        el: '#video',

        data: {
            Videos: [],
            editVideo: null,

            video: {
                VideoTitle: '',
                level: '',
                VideoUrl: '',


            }
        },
        created: function () {
            var self = this;

            // Fetch list of Questions
            axios.get("/api/VideoApi")
                .then(function (response) {

                    self.Videos = response.data;
                });
        },
        methods: {
            addQuestion: function () {

                var self = this;

                if (self.video.VideoTitle.length === 0 || self.video.VideoUrl.length === 0)
                    return;

                axios.post('/api/VideoApi', self.video).then(function (response) {
                    self.Videos.push(response.data);

                    self.video.VideoTitle = '',
                        self.video.level = '',

                        self.video.VideoUrl = ''

                })

            },

            remove: function (id) {
                var self = this;

                axios.delete('/api/VideoApi/' + id).then(function (response) {

                    self.Videos = self.Videos.filter(function (p) {
                        return p.id !== id
                    })

                })
            },
            Edit: function (id) {
                var self = this;


                axios({
                    method: 'put',
                    url: '/api/VideoApi/' + id,
                    data: {
                        id: id,
                        VideoTitle: self.video.VideoTitle,
                        level: self.video.level,
                        VideoUrl: self.video.VideoUrl,

                    }
                })
                    .then(function (response) {
                        // handle success
                        self.video.VideoTitle = '',
                            self.video.level = '',
                            self.video.VideoUrl = ''

                    })
                    .catch(function (error) {
                        // handle error
                        console.log(error);
                    })
                    .then(function () {
                        // Always execute
                        axios.get('/api/VideoApi')
                            .then(function (response) {
                                self.Videos = response.data;
                            })
                    });
            },

        }

    })

});