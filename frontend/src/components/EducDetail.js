import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import Api from "../utils/Api";


const EducDetail = ({setIsLoading, setIsStatusInfo}) => {
    const { empid } = useParams();

    const [empdata, empdatachange] = useState({});

    useEffect(() => {
      setIsLoading(true);
      Api.getOneEducator(empid)
        .then(data => {
          empdatachange(data);
          setIsStatusInfo({
            isOpen: true,
            successful:true,
            text: "Данные получены!",
          });
        })
        .catch(error => {
          setIsStatusInfo({
            isOpen: true,
            successful:false,
            text: error,
          })
        })
        .finally(() => setIsLoading(false));
    }, []);

    return (
        <div>
            {/* <div className="row">
                <div className="offset-lg-3 col-lg-6"> */}

               <div className="container">

            <div className="card row" style={{ "textAlign": "left" }}>
                <div className="card-title">
                    <h2>Educator Create</h2>
                </div>
                <div className="card-body"></div>

                {empdata &&
                    <div>
                        <h2>The Educator name is : <b>{empdata.Name}</b>  ({empdata.Id})</h2>
                        <h3>Details</h3>
                        <h5>Email is : {empdata.Email}</h5>
                        <h5>Age is : {empdata.Age}</h5>
                        <Link className="btn btn-danger" to="/">Back to Listing</Link>
                    </div>
                }
            </div>
            </div>
            {/* </div>
            </div> */}
        </div >
    );
}

export default EducDetail;