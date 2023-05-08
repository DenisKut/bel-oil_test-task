import { useEffect, useState } from "react";
import { Link, useHistory, useParams } from "react-router-dom";
import Api from "../utils/Api";

const EducEdit = ({setIsLoading, setIsStatusInfo}) => {
    const { empid } = useParams();
    const history = useHistory();
    //const [empdata, empdatachange] = useState({});

    useEffect(() => {
      setIsLoading(true);
      Api
        .getOneEducator(empid)
        .then((resp) => {
          idchange(resp.Id);
          namechange(resp.Name);
          emailchange(resp.Email);
          agechange(resp.Age);
      }).catch(error => {
        setIsStatusInfo({
          isOpen: true,
          successful:false,
          text: error,
        })
      })
        .finally(() => setIsLoading(false));
    }, []);

    const[id,idchange]=useState("");
    const[name,namechange]=useState("");
    const[email,emailchange]=useState("");
    const[age,agechange]=useState("");
    const[validation,valchange]=useState(false);


    const handlesubmit=(e)=>{
      e.preventDefault();
      setIsLoading(true);
      const empdata={id,name,email,age};

      Api.setEducatorInfo(empdata)
      .then((res)=>{
        setIsStatusInfo({
          isOpen: true,
          successful:true,
          text: 'Saved successfully.',
        });
        history.push("/");
      })
      .catch(error => {
        setIsStatusInfo({
          isOpen: true,
          successful:false,
          text: error,
        })
      })
      .finally(() => setIsLoading(false));
    }

    return (
        <div>

        <div className="row">
            <div className="offset-lg-3 col-lg-6">
                <form className="container" onSubmit={handlesubmit}>

                    <div className="card" style={{"textAlign":"left"}}>
                        <div className="card-title">
                            <h2>Educator Edit</h2>
                        </div>
                        <div className="card-body">

                            <div className="row">

                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <label>ID</label>
                                        <input value={id} disabled="disabled" className="form-control"></input>
                                    </div>
                                </div>

                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <label>Name</label>
                                        <input required value={name} onMouseDown={e=>valchange(true)} onChange={e=>namechange(e.target.value)} className="form-control"></input>
                                    {name.length==0 && validation && <span className="text-danger">Enter the name</span>}
                                    </div>
                                </div>

                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <label>Email</label>
                                        <input value={email} onChange={e=>emailchange(e.target.value)} className="form-control"></input>
                                    </div>
                                </div>

                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <label>Age</label>
                                        <input value={age} onChange={e=>agechange(e.target.value)} className="form-control"></input>
                                    </div>
                                </div>


                                <div className="col-lg-12">
                                    <div className="form-group">
                                       <button className="btn btn-success" type="submit">Save</button>
                                       <Link to="/" className="btn btn-danger">Back</Link>
                                    </div>
                                </div>

                            </div>

                        </div>

                    </div>

                </form>

            </div>
        </div>
    </div>
     );
}

export default EducEdit;