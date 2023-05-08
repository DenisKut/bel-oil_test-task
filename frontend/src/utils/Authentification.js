const authUrl = "http://localhost:5045/api/Authentification"
//const authUrl = "http://localhost:3000"
class Authentification{
  constructor({link, headers}) {
    this._link = link;
    this._headers = headers;
  }

  _getAuthHeaders() {
    const jwt = localStorage.getItem('jwt');
    return {
      'Authorization': `Bearer ${jwt}`,
      ...this._headers,
    }
  }

  async checkErrors(res) {
    const resMessage = await res.json();
    if (res.ok) {
      return resMessage;
    }
    return Promise.reject(`Ошибка ${res.status}: ${resMessage.message}`);
  }

  register(name, email, password) {
    return fetch(`${this._link}/Register`, {
      method: 'POST',
      headers: this._headers,
      body: JSON.stringify({
        "Name": name,
        "Password": password,
        "Email": email
      })
    })
    .then(this.checkErrors)
  }

  login(email, password) {
    return fetch(`${this._link}/Login`, {
      method: 'POST',
      headers: this._headers,
      body: JSON.stringify({
        Password: password,
        Email: email
      })
    })
    .then(this.checkErrors);
  }

  setUserInfo(name, email, UserId) {
    return fetch(`${this._link}/ChangeUserInfo`, {
      method: 'PATCH',
      headers: this._getAuthHeaders(),
      body: JSON.stringify({
        Name: name,
        Email: email,
        UserId: UserId
      })
    })
      .then(this.checkErrors)
  }

  getUserInfo(id) {
    return fetch(`${this._link}/GetUserInfo`, {
      method: 'GET',
      headers: this._getAuthHeaders(),
      body: JSON.stringify({
        id
      })
    })
      .then(this.checkErrors);
  }

  checkToken(jwt) {
    return fetch(`http://localhost:5045/api/Educator`, {
      method: 'GET',
      headers: {
        ...this._headers,
        "Authorization" : `Bearer ${jwt}`
      }
    })
    .then(this.checkErrors)
  }
}

export default new Authentification({
  link: authUrl,
  headers: { "Content-Type": "application/json" }
});
