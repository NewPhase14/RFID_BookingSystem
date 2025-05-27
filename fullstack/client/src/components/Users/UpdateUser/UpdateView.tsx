import React, {useState} from 'react'
import {useAtom} from "jotai";
import {JwtAtom, UserAtom, UsersAtom} from "../../../atoms/atoms.ts";
import {userClient} from "../../../apiControllerClients.ts";
import { UserUpdateRequestDto} from "../../../models/generated-client.ts";
import toast from "react-hot-toast";
import {useNavigate} from "react-router";
import {UserRoute} from "../../../helpers/routeConstants.tsx";

export const UpdateView = () => {
    const [user] = useAtom(UserAtom);
    const [jwt] = useAtom(JwtAtom);
    const [users, setUsers] = useAtom(UsersAtom);
    const [id] = useState(user!.id);
    const [firstName, setFirstName] = useState(user!.firstName);
    const [lastName, setLastName] = useState(user!.lastName);
    const [rfid, setRfid] = useState(user!.rfid);
    const navigate = useNavigate();

    const userUpdateRequestDto: UserUpdateRequestDto = {
        id: id,
        firstName: firstName,
        lastName: lastName,
        rfid: rfid,
    }
    return (
        <div className="flex items-center justify-center">
            <div className="bg-background-grey border border-white/10 rounded-2xl p-10 max-w-4xl w-full">
                <h2 className="text-center text-white text-xl mb-6">Update User</h2>
                <div className="grid grid-cols-2 gap-6">
                    <div>
                        <label className="block mb-2 text-sm text-text-grey">RFID</label>
                        <input
                            type="text"
                            value={rfid}
                            onChange={(e) => setRfid(e.target.value)}
                            required
                            className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                        />
                    </div>

                    <div>
                        <label className="block mb-2 text-sm text-text-grey">Email</label>
                        <input
                            disabled
                            value={user!.email}
                            required
                            className="w-full px-4 py-3 rounded-md text-white/10 border border-white/10 bg-textfield-grey/20 focus:outline-white"
                        />
                    </div>

                    <div>
                        <label className="block mb-2 text-sm text-text-grey">First Name</label>
                        <input
                            value={firstName}
                            onChange={(e) => setFirstName(e.target.value)}
                            required
                            className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                        />
                    </div>

                    <div>
                        <label className="block mb-2 text-sm text-text-grey">Last Name</label>
                        <input
                            value={lastName}
                            onChange={(e) => setLastName(e.target.value)}
                            required
                            className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                        />
                    </div>

                    <div>
                        <label className="block mb-2 text-sm text-text-grey">Created at</label>
                        <input
                            disabled
                            value={user!.createdAt}
                            className="w-full px-4 py-3 rounded-md text-white/10 border border-white/10 bg-textfield-grey/20 focus:outline-white"
                        />
                    </div>

                    <div>
                        <label className="block mb-2 text-sm text-text-grey">Updated at</label>
                        <input
                            disabled
                            value={user!.updatedAt}
                            className="w-full px-4 py-3 rounded-md text-white/10 border border-white/10 bg-textfield-grey/20 focus:outline-white"
                        />
                    </div>

                    <div className="col-span-2">
                        <label className="block mb-2 text-sm text-text-grey">Role</label>
                        <input
                            disabled
                            value={user!.roleName}
                            className="w-full px-4 py-3 rounded-md text-white/10 border border-white/10 bg-textfield-grey/20 focus:outline-white"
                        />
                    </div>

                    <div className="col-span-2">
                        <button
                            onClick={() => {
                                userClient.updateUser(userUpdateRequestDto, jwt)
                                    .then(r => {
                                        toast.success("User Updated Successfully");
                                        const updatedUsers = users.filter((u) => u.id !== r.id);
                                        setUsers([...updatedUsers, r]);
                                        navigate(UserRoute);
                                    })
                                    .catch(() => {
                                        toast.error("User Update Failed");
                                    });
                            }}
                            className="w-full py-3 rounded-md text-[var(--color-background-black)] bg-[--color-button-grey] hover:bg-blue-500 hover:text-white"
                        >
                            Update User
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
}
