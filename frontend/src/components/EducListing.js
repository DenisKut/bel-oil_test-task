import { useEffect, useState } from "react";
import { Link, useHistory } from "react-router-dom";
import Api from "../utils/Api";

const EducListing = ({setIsLoading, setIsStatusInfo}) => {
    const [empdata, empdatachange] = useState(null);
    const history = useHistory();

    const LoadDetail = (id) => {
        history.push("/educ/detail/" + id);
    }
    const LoadEdit = (id) => {
      history.push("/educ/edit/" + id);
    }
    const Removefunction = (data) => {
        if (window.confirm('Do you want to remove?')) {
            setIsLoading(true);
            Api.deleteEducator(data)
            .then((res) => {
                window.location.reload();
                setIsStatusInfo({
                    isOpen: true,
                    successful:true,
                    text: "Данные удалены!",
                });
            })
            .catch(error => {
              setIsStatusInfo({
                isOpen: true,
                successful:false,
                text: error,
              });
            })
            .finally(() => setIsLoading(false));
        }
    }

    useEffect(() => {
        setIsLoading(true);
        Api
          .getAllEducators()
          .then(data => {
            empdatachange(data);
          })
          .catch(error => {
            setIsStatusInfo({
              isOpen: true,
              successful:false,
              text: error,
            })
          })
    },[]);

    return (
        <div className="container">
            <div className="card">
                <div className="card-title">
                    <h2>Educators Listing</h2>
                </div>
                <div className="card-body">
                    <div className="divbtn">
                        <Link to="educ/create" className="btn btn-success">Add New (+)</Link>
                    </div>
                    <table className="table table-bordered">
                        <thead className="bg-dark text-white">
                            <tr>
                                <td>ID</td>
                                <td>Name</td>
                                <td>Email</td>
                                <td>Age</td>
                            </tr>
                        </thead>
                        <tbody>

                            {empdata &&
                                JSON.stringify(empdata).map(item => (
                                    <tr key={item.Id}>
                                        <td>{item.Id}</td>
                                        <td>{item.Name}</td>
                                        <td>{item.Email}</td>
                                        <td>{item.Age}</td>
                                        <td><a onClick={() => { LoadEdit(item.Id) }} className="btn btn-success">Edit</a>
                                            <a onClick={() => { Removefunction(item) }} className="btn btn-danger">Remove</a>
                                            <a onClick={() => { LoadDetail(item.id) }} className="btn btn-primary">Details</a>
                                        </td>
                                    </tr>
                                ))
                            }

                        </tbody>

                    </table>
                </div>
            </div>
        </div>
    );
}

export default EducListing;