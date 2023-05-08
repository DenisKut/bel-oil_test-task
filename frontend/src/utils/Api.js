class Api {
  constructor({link, headers}) {
    this._link = link;
    this._headers = headers;
  }

  _getHeaders() {
    const jwt = localStorage.getItem('jwt');
    return {
      'Authorization': `Bearer ${jwt}`,
      ...this._headers,
    }
  }

  //Children
  getAllChildren() {
    return fetch(`${this._link}/Child`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  getOneChild(data) {
    return fetch(`${this._link}/Child/${data.id}`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  addChild(data) {
    return fetch(`${this._link}/Child`, {
      method: 'POST',
      headers: this._getHeaders(),
      body: JSON.stringify({
        name: data.name,
        age: data.age,
        passport: data.passport,
        stateOfHealth: data.stateOfHealth,
        groupId: data.groupId,
        weight: data.weight,
        growth: data.growth,
        parentInfoId: data.parentInfoId,
        contractId: data.contractId,
        region: data.region
      })
    })
      .then(this.checkErrors);
  }

  setChildInfo(data) {
    return fetch(`${this._link}/Child/${data.id}`, {
      method: 'PATCH',
      headers: this._getHeaders(),
      body: JSON.stringify({
        name: data.name,
        age: data.age,
        passport: data.passport,
        stateOfHealth: data.stateOfHealth,
        groupId: data.groupId,
        weight: data.weight,
        growth: data.growth,
        parentInfoId: data.parentInfoId,
        contractId: data.contractId,
        region: data.region
      })
    })
      .then(this.checkErrors)
  }

  deleteChild(data) {
    return fetch(`${this._link}/Child`, {
      method: 'DELETE',
      headers: this._getHeaders(),
      body: JSON.stringify({
        id: data.id
      })
    })
      .then(this.checkErrors);
  }

  getClearZoneChildren() {
    return fetch(`${this._link}/Child/ChildrenWithoutDirtyZone`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  getChildrenWithPays(firstDate, lastDate) {
    return fetch(`${this._link}/Child/InfoAboutPaysByDates`, {
      method: 'GET',
      headers: this._getHeaders(),
      body: JSON.stringify({
        FirstDate: firstDate,
        LastDate: lastDate
      })
    })
      .then(this.checkErrors);
  }

  ///Contract
  getAllContracts() {
    return fetch(`${this._link}/Contract`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  getOneContract(data) {
    return fetch(`${this._link}/Contract/${data.id}`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  addContract(data) {
    return fetch(`${this._link}/Contract`, {
      method: 'POST',
      headers: this._getHeaders(),
      body: JSON.stringify({
        headId: data.headId,
        date: data.date,
        description: data.description
      })
    })
      .then(this.checkErrors);
  }

  setContractInfo(data) {
    return fetch(`${this._link}/Contract/${data.id}`, {
      method: 'PATCH',
      headers: this._getHeaders(),
      body: JSON.stringify({
        headId: data.headId,
        date: data.date,
        description: data.description
      })
    })
      .then(this.checkErrors)
  }

  deleteContract(data) {
    return fetch(`${this._link}/Contract`, {
      method: 'DELETE',
      headers: this._getHeaders(),
      body: JSON.stringify({
        id: data.id
      })
    })
      .then(this.checkErrors);
  }

  ///Educator
  getAllEducators() {
    return fetch(`${this._link}/Educator`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  getOneEducator(data) {
    return fetch(`${this._link}/Educator/${data}`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  addEducator(data) {
    return fetch(`${this._link}/Educator`, {
      method: 'POST',
      headers: this._getHeaders(),
      body: JSON.stringify({
        name: data.name,
        age: data.age,
        email: data.email
      })
    })
      .then(this.checkErrors);
  }

  setEducatorInfo(data) {
    return fetch(`${this._link}/Educator/${data.id}`, {
      method: 'PATCH',
      headers: this._getHeaders(),
      body: JSON.stringify({
        name: data.name,
        age: data.age,
        email: data.email
      })
    })
      .then(this.checkErrors)
  }

  deleteEducator(data) {
    return fetch(`${this._link}/Educator`, {
      method: 'DELETE',
      headers: this._getHeaders(),
      body: JSON.stringify({
        id: data.id
      })
    })
      .then(this.checkErrors);
  }

  ///Group
  getAllGroups() {
    return fetch(`${this._link}/Group`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  getOneGroup(data) {
    return fetch(`${this._link}/Group/${data.id}`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  addGroup(data) {
    return fetch(`${this._link}/Group`, {
      method: 'POST',
      headers: this._getHeaders(),
      body: JSON.stringify({
        name: data.name,
        educatorId: data.educatorId
      })
    })
      .then(this.checkErrors);
  }

  setGroupInfo(data) {
    return fetch(`${this._link}/Group/${data.id}`, {
      method: 'PATCH',
      headers: this._getHeaders(),
      body: JSON.stringify({
        name: data.name,
        educatorId: data.educatorId
      })
    })
      .then(this.checkErrors)
  }

  deleteGroup(data) {
    return fetch(`${this._link}/Group`, {
      method: 'DELETE',
      headers: this._getHeaders(),
      body: JSON.stringify({
        id: data.id
      })
    })
      .then(this.checkErrors);
  }

  ///Head
  getAllHeads() {
    return fetch(`${this._link}/Head`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  getOneHead(data) {
    return fetch(`${this._link}/Head/${data.id}`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  addHead(data) {
    return fetch(`${this._link}/Head`, {
      method: 'POST',
      headers: this._getHeaders(),
      body: JSON.stringify({
        name: data.name,
        age: data.age,
        email: data.email
      })
    })
      .then(this.checkErrors);
  }

  setHeadInfo(data) {
    return fetch(`${this._link}/Head/${data.id}`, {
      method: 'PATCH',
      headers: this._getHeaders(),
      body: JSON.stringify({
        name: data.name,
        age: data.age,
        email: data.email
      })
    })
      .then(this.checkErrors)
  }

  deleteHead(data) {
    return fetch(`${this._link}/Head`, {
      method: 'DELETE',
      headers: this._getHeaders(),
      body: JSON.stringify({
        id: data.id
      })
    })
      .then(this.checkErrors);
  }

  ///ParentInfo
  getAllParentInfos() {
    return fetch(`${this._link}/ParentInfo`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  getOneParentInfo(data) {
    return fetch(`${this._link}/ParentInfo/${data.id}`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  addParentInfo(data) {
    return fetch(`${this._link}/ParentInfo`, {
      method: 'POST',
      headers: this._getHeaders(),
      body: JSON.stringify({
        mother: data.mother,
        father: data.father,
        fatherProfession: data.fatherProfession,
        motherProfession: data.motherProfession,
        motherAge: data.motherAge,
        fatherAge: data.fatherAge
      })
    })
      .then(this.checkErrors);
  }

  setParentInfo(data) {
    return fetch(`${this._link}/ParentInfo/${data.id}`, {
      method: 'PATCH',
      headers: this._getHeaders(),
      body: JSON.stringify({
        mother: data.mother,
        father: data.father,
        fatherProfession: data.fatherProfession,
        motherProfession: data.motherProfession,
        motherAge: data.motherAge,
        fatherAge: data.fatherAge
      })
    })
      .then(this.checkErrors)
  }

  deleteParentInfo(data) {
    return fetch(`${this._link}/ParentInfo`, {
      method: 'DELETE',
      headers: this._getHeaders(),
      body: JSON.stringify({
        id: data.id
      })
    })
      .then(this.checkErrors);
  }

  getParentsWithManyChilds() {
    return fetch(`${this._link}/ParentInfo/GetParentsWithManyChildren`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  ///Payment
  getAllPayments() {
    return fetch(`${this._link}/Payment`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  getOnePayment(data) {
    return fetch(`${this._link}/Payment/${data.id}`, {
      method: 'GET',
      headers: this._getHeaders()
    })
      .then(this.checkErrors);
  }

  addPayment(data) {
    return fetch(`${this._link}/Payment`, {
      method: 'POST',
      headers: this._getHeaders(),
      body: JSON.stringify({
        dateOfPayment: data.dateOfPayment,
        typeOfPayment: data.typeOfPayment,
        childId: data.childId
      })
    })
      .then(this.checkErrors);
  }

  setPayment(data) {
    return fetch(`${this._link}/Payment/${data.id}`, {
      method: 'PATCH',
      headers: this._getHeaders(),
      body: JSON.stringify({
        dateOfPayment: data.dateOfPayment,
        typeOfPayment: data.typeOfPayment,
        childId: data.childId
      })
    })
      .then(this.checkErrors)
  }

  deletePayment(data) {
    return fetch(`${this._link}/Payment`, {
      method: 'DELETE',
      headers: this._getHeaders(),
      body: JSON.stringify({
        id: data.id
      })
    })
      .then(this.checkErrors);
  }

  getPaymentsForAmount(data) {
    return fetch(`${this._link}/Payment/PaysForAmount`, {
      method: 'GET',
      headers: this._getHeaders(),
      body: JSON.stringify({
        amount: data
      })
    })
      .then(this.checkErrors);
  }

  checkErrors(res) {
    if (res.ok) {
      return res.json();
    }
    return Promise.reject(`Ошибка: ${res.status}`);
  }

}




export default new Api({
  link: 'http://localhost:5045/api',
  //link: "http://localhost:3000",
  headers: {
    'Content-Type': 'application/json'
  }
});
