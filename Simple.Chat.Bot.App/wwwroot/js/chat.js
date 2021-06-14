"use strict";

//Disable send button until connection is established
// document.getElementById("sendMessageButton").disabled = true;
document.getElementById("sendMessageButton").disabled = true;
const buildChatMessage = (message, nickname, datePosted, isOwn) => {
  return `
  <li class="list-group-item">
    <div class="card-body">
      <h5 class="card-title">${isOwn ? "You" : nickname}</h5>
      <h6 class="card-subtitle mb-2 text-muted">${datePosted}</h6>
      <p class="card-text"> ${message}</p>
  </div>
</li>`;
};

const connection = new signalR.HubConnectionBuilder()
  .withUrl("/Home/Index")
  .withAutomaticReconnect()
  .build();

connection.on("ReceiveMessage", (chatMessage) => {
  let nicknameInput = document.getElementById("nickNameInput");
  const message = buildChatMessage(
    chatMessage.message,
    chatMessage.nickname,
    chatMessage.datePostedFormatted,
    chatMessage.nickname == nicknameInput.value
  );
  const chatList = document.getElementById("chatList");
  appendHtml(chatList, message);
  scrollDown(chatList);
  keepChatListCount(chatList, 50);
});

const scrollDown = (element) => {
  element.scrollTop = element.scrollHeight;
}

const appendHtml = (element, html) => {
  element.innerHTML += html;
}

const keepChatListCount = (chatList, count) => {
  let children = chatList.getElementsByTagName('li');
  let removeCount = children.length - count;
  while(removeCount > 0){
    chatList.removeChild(children[0]);
    removeCount--;
  }
}

connection.closedCallbacks.push(() => {
  document.getElementById("sendMessageButton").disabled = true;
  console.info("disconnected from chat room...");
});
connection.reconnectedCallbacks.push(() => {
  document.getElementById("sendMessageButton").disabled = false;
  console.info("connected to chat room!");
});

connection
  .start()
  .then(() => {
    document.getElementById("sendMessageButton").disabled = false;
  })
  .catch((err) => {
    document.getElementById("sendMessageButton").disabled = true;
    return console.error(err.toString());
  });
const sendMessage = (message) => {
  connection.invoke("sendMessage", message).catch((err) => {
    return console.error(err.toString());
  });
};

document
  .getElementById("sendMessageButton")
  .addEventListener("click", (event) => {
    let messageInput = document.getElementById("messageInput");
    if(messageInput.value.length > 0){
      sendMessage(messageInput.value);
    }
    messageInput.value = "";
    event.preventDefault();
  });