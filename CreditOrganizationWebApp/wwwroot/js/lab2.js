const uri = 'api/Clients';
const uri2 = 'api/Clients/Debtors';
let clients = [];
function getClients() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayClients(data))
        .catch(error => console.error('Unable to get clients.', error));
}
function getClients2() {
    fetch(uri2)
        .then(response => response.json())
        .then(data => _displayClients(data))
        .catch(error => console.error('Unable to get clients.', error));
}
function addClient() {
    const addFirstNameTextbox = document.getElementById('add-firstName');
    const addSecondNameTextbox = document.getElementById('add-secondName');
    const addBirthDateTextbox = document.getElementById('add-birthDate');
    const client = {
        firstName: addFirstNameTextbox.value.trim(),
        secondName: addSecondNameTextbox.value.trim(),
        birthDate: addBirthDateTextbox.value.trim(),
    };
    fetch(uri, {
        method: 'POST',
        headers: {

            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(client)
    })
        .then(response => response.json())
        .then(() => {
            getClients();
            addFirstNameTextbox.value = '';
            addSecondNameTextbox.value = '';
            addBirthDateTextbox.value = '';
        })
        .catch(error => console.error('Unable to add client.', error));
}
function deleteClient(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getClients())
        .catch(error => console.error('Unable to delete client.', error));
}
function displayEditForm(id) {
    const client = clients.find(client => client.id === id);
    document.getElementById('edit-id').value = client.id;
    document.getElementById('edit-firstName').value = client.firstName;
    document.getElementById('edit-secondName').value = client.secondName;
    document.getElementById('edit-birthDate').value = client.birthDate;
    document.getElementById('editForm').style.display = 'block';
}
function updateClient() {
    const clientId = document.getElementById('edit-id').value;
    const client = {
        id: parseInt(clientId, 10),
        firstName: document.getElementById('edit-firstName').value.trim(),
        secondName: document.getElementById('edit-secondName').value.trim(),
        birthDate: document.getElementById('edit-birthDate').value.trim()
    };
    fetch(`${uri}/${clientId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(client)
    })
        .then(() => getClients())
        .catch(error => console.error('Unable to update client.', error));
    closeInput();
    return false;
}
function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}
function _displayClients(data) {
    const tBody = document.getElementById('clients');

    tBody.innerHTML = '';
    const button = document.createElement('button');
    data.forEach(client => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Редагувати';
        editButton.setAttribute('onclick', `displayEditForm(${client.id})`);
        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Видалити';
        deleteButton.setAttribute('onclick', `deleteClient(${client.id})`);
        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(client.firstName);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNode2 = document.createTextNode(client.secondName);
        td2.appendChild(textNode2);

        let td3 = tr.insertCell(2);
        let textNode3 = document.createTextNode(client.birthDate);
        td3.appendChild(textNode3);

        let td4 = tr.insertCell(3);
        td4.appendChild(editButton);

        let td5 = tr.insertCell(4);
        td5.appendChild(deleteButton);
    });
    clients = data;
}