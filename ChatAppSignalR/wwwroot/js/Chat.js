var name = "";


var urlParams = new URLSearchParams(window.location.search);
//console.log(urlParams.has('post')); // true
name = urlParams.get('userName'); // "edit"


var connection = new signalR.HubConnectionBuilder().withUrl('/chatHub').build();

connection.on('RecieveMessage', renderMessage);
connection.on('ShowContactList', updateContactList);

connection.start();


function ready() {
    var chatForm = document.getElementById('chatform');
    chatForm.addEventListener('submit',
        function (e) {
            e.preventDefault();
            var text = document.getElementById('messagetext').value;   ///messagetext
            document.getElementById('messagetext').value = '';
            sendMessage(text);
        });
}


function updateContactList(name, sendAt, connections) {
    var contactList = document.getElementById('ContactListData');
    connections.forEach(function (item) {
        var newItem = document.createElement('li');
        contactList.append('<li>' + item + '</li>');
    });
    console.log(connections);
}



function renderMessage(name, sendAt, text) {
    var nameSpan = document.createElement('span');
    nameSpan.className = 'name';
    nameSpan.textContent = name;

    var timeSpan = document.createElement('span');
    timeSpan.className = 'time';
    var timeFriendly = moment(sendAt).format('H:mm');
    timeSpan.textContent = timeFriendly;

    var headerDiv = document.createElement('div');
    headerDiv.appendChild(nameSpan);
    headerDiv.appendChild(timeSpan);

    var messageDiv = document.createElement('div');
    messageDiv.className = 'message';
    messageDiv.textContent = text;

    var newItem = document.createElement('li');
    newItem.appendChild(headerDiv);
    newItem.appendChild(messageDiv);

    var chatHistory = document.getElementById('chatHistory');
    chatHistory.appendChild(newItem);
    chatHistory.scrollTop = chatHistory.scrollHeight - chatHistory.clientHeight;
}


function sendMessage(text) {
    console.log(text);
    //if (text && text.lenght) {

    connection.invoke('SendMessage', name, text);
    //}
}


document.addEventListener('DOMContentLoaded', ready);