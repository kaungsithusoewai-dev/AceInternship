const tblBlog = "blogs";
let blogId = null;
getBlogTable();

//createBlog();
//updateBlog("e81d5042-3184-4ebe-bb3b-112eeed3e94e", "kst", "kst1", "kst2");
//deleteBlog("e81d5042-3184-4ebe-bb3b-112eeed3e94e", "kst", "kst1", "kst2");
function readBlog() {
  let lst = getBlog();
  console.log(lst);
}

function createBlog(title, author, content) {
  let lst = getBlog();

  const requestModel = {
    id: uuidv(),
    title: title,
    author: author,
    content: content,
  };

  lst.push(requestModel);

  const jsonBLog = JSON.stringify(lst);
  localStorage.setItem(tblBlog, jsonBLog);
  //localStorage.setItem("blogs" ,requestModel);

  successMessage("Saving successful.");
  clearControls();
}
function editBlog(id) {
  let lst = getBlog();

  const items = lst.filter(x => x.id === id);
  console.log(items);

  console.log(items.length);

  if (items.length == 0) {
    console.log("no data found.");
    errorMessage("No data found");
    return;
  }
  let item = items[0];
  blogId = item.id;
  $("#textTitle").val(item.title);
  $("#textAuthor").val(item.author);
  $("#textContent").val(item.content);
  $("#textTitle").focus();
}

function updateBlog(id, title, author, content) {
  let lst = getBlog();

  const items = lst.filter(x => x.id === id);
  console.log(items);

  console.log(items.length);

  if (items.length == 0) {
    console.log("no data found");
    errorMessage("No data found");
    return;
  }
  const item = items[0];
  item.title = title;
  item.author = author;
  item.content = content;

  const index = lst.findIndex(x => x.id === id);
  lst[index] = item;

  const jsonBLog = JSON.stringify(lst);
  localStorage.setItem(tblBlog, jsonBLog);
  successMessage("Update Successfully.");
}

function deleteBlog(id) {
  let result = Notiflix;
  if (!result) return;
  Notiflix.Confirm.show(
    "Notiflix Confirm",
    "Do you agree with me?",
    "Yes",
    "No",
    function okCb() {
     let lst = getBlog();

     const items = lst.filter((x) => x.id === id);
     if (items.length == 0) {
       console.log("no data found");
       return;
     }

     lst = lst.filter((x) => x.id !== id);
     const jsonBLog = JSON.stringify(lst);
     localStorage.setItem(tblBlog, jsonBLog);
     successMessage("Delete Successfully");
     getBlogTable();
    },
    function cancelCb() {
      errorMessage("try again");
      ;
    },
  );
}

function uuidv() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
    (
      +c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (+c / 4)))
    ).toString(16)
  );
}
function getBlog() {
  const blogs = localStorage.getItem(tblBlog);
  console.log(blogs);

  let lst = [];
  if (blogs !== null) {
    lst = JSON.parse(blogs);
  }
  return lst;
}
$("#btnSave").click(function () {
  const title = $("#textTitle").val();
  const author = $("#textAuthor").val();
  const content = $("#textContent").val();
  if(!title || !author || !content) {
            errorMessage("Required.");
            return;}
            Notiflix.Loading.dots();
            setTimeout(() => {
            Notiflix.Loading.remove();

  if (blogId === null) {
    createBlog(title, author, content);
    successMessage("Successfully")
  } else {
    updateBlog(blogId, title, author, content);
    blogId = null;
    successMessage('Update Success!');
  }
  clearControls();
  getBlogTable();
},3000);})

function successMessage(message) {
  Swal.fire({
    title: "Success",
    text: message,
    icon: "success",
  });
  
}
function errorMessage(message) {
 Swal.fire({
   title: "Error",
   text: message,
   icon: "error",
 });
}

function clearControls() {
  $("#textTitle").val("");
  $("#textAuthor").val("");
  $("#textContent").val("");
  $("#textTitle").focus();
}

function getBlogTable() {
  const lst = getBlog();
  let count = 0;
  let htmlRows = '';
  lst.forEach(item => {
    const htmlRow = `
    <tr>
    <td>
    <button type="button" class="btn btn-warning" onclick="editBlog('${item.id
    }')">Edit</button>
    <button type="button" class="btn btn-danger" onclick="deleteBlog('${
      item.id
    }')">Delete</button>
    </td>
    <td>${++count}</td>
    <td>${item.title}</td>
    <td>${item.author}</td>
    <td>${item.content}</td>
    </tr>`;
    htmlRows += htmlRow;
  });
  $("#tbody").html(htmlRows);
}

