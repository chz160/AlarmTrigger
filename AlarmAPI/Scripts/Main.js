$(document).ready(function () {
    // Declare a proxy to reference the hub. 
    var chat = $.connection.twitterUpdateHub;
    console.log(chat);
    // Create a function that the hub can call to broadcast messages.
    chat.client.sendNewTweet = function (tweet) {
        console.log(tweet);
        ShowTweet(tweet);
    };

    // Start the connection
    $.connection.hub.start().done(function () { });


    function ShowTweet(tweet) {
        $("#show-tweet").html(tweet.HTML);
    }
});