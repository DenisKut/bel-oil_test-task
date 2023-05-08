import "../../node_modules/bootstrap/dist/css/bootstrap-grid.min.css"
import { Route, Switch} from 'react-router-dom'
import EducListing from './EducListing';
import EducCreate from './EducCreate';
import EducDetail from './EducDetail';
import EducEdit from './EducEdit';

function Educators({setIsLoading, setIsStatusInfo}) {
  return(
    <Switch>
      <Route path='/' element={<EducListing
        setIsLoading={setIsLoading}
        setIsStatusInfo={setIsStatusInfo} />}
      >
      </Route>
      <Route path='/educ/create' element={
      <EducCreate
        setIsLoading={setIsLoading}
        setIsStatusInfo={setIsStatusInfo}
      />}></Route>

      <Route path='/educ/detail/:educid' element={
      <EducDetail
        setIsLoading={setIsLoading}
        setIsStatusInfo={setIsStatusInfo}
      />}></Route>
      <Route path='/educ/edit/:educid' element={
      <EducEdit
        setIsLoading={setIsLoading}
        setIsStatusInfo={setIsStatusInfo}
      />}></Route>

    </Switch>
  )
}

export default Educators;