import {useAtom} from "jotai";
import {JwtAtom, UserAtom, UsersAtom} from "../../../atoms/atoms.ts";
import {FiEdit, FiTrash} from "react-icons/fi";
import {userClient} from "../../../apiControllerClients.ts";
import toast from "react-hot-toast";
import {useNavigate} from "react-router";
import {UpdateUserRoute} from "../../../helpers/routeConstants.tsx";

export const Table = () => {
    const [users, setUsers] = useAtom(UsersAtom);
    const [, setSelectedUser] = useAtom(UserAtom);
    const [jwt] = useAtom(JwtAtom);
    const navigate = useNavigate();

    return (
        <div className="overflow-x-auto rounded-box border border-base-content/5 mt-4 mx-4">
            <table className="table">
                <thead>
                <tr>
                    <th>Email</th>
                    <th>First name</th>
                    <th>Last name</th>
                    <th>Rfid</th>
                    <th>Created at</th>
                    <th>Updated at</th>
                    <th>Edit User</th>
                    <th>Delete User</th>
                </tr>
                </thead>
                <tbody>
                {users.map((user) => (
                    <tr key={user.id}>
                        <td>{user.email}</td>
                        <td>{user.firstName}</td>
                        <td>{user.lastName}</td>
                        <td>{user.rfid}</td>
                        <td>{user.createdAt}</td>
                        <td>{user.updatedAt}</td>
                        <td><button onClick={() => {
                            userClient.getUserByEmail(user.email, jwt).then((user) => {
                                setSelectedUser(user);
                                navigate(UpdateUserRoute);
                            })
                        }} className="btn btn-sm bg-blue-500"><FiEdit/></button></td>
                        <td><button onClick={() => { userClient.deleteUser(user.id, jwt).then(() => {
                            toast.success("User deleted successfully");
                            setUsers(users.filter((u) => u.id !== user.id));
                        })}} className="btn btn-sm bg-red-800"><FiTrash/></button></td>
                    </tr>
                ))}

                </tbody>
            </table>
        </div>
    )
}
